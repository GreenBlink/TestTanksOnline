using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class WallProvider : MonoProvider<WallComponent>
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" && GetData().isDestroy)
        {
            Tilemap tilemap = GetComponent<Tilemap>();
            var tilePos = tilemap.WorldToCell(collision.gameObject.transform.position);

            CheakPos(tilemap, ref tilePos, collision.gameObject);
            tilemap.SetTile(tilePos, null);
        }
    }

    private void CheakPos(Tilemap tilemap, ref Vector3Int tilePos, GameObject collision)
    {
        if (tilemap.GetTile(tilePos) == null)
        {
            tilePos = tilemap.WorldToCell(new Vector3(collision.transform.position.x - 0.3f, collision.transform.position.y - 0.3f));
        }

        if (tilemap.GetTile(tilePos) == null)
        {
            tilePos = tilemap.WorldToCell(new Vector3(collision.transform.position.x + 0.3f, collision.transform.position.y + 0.3f));
        }
    }
}