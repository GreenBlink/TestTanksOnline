using Morpeh;
using Photon.Pun;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class ShootProvider : MonoProvider<ShootComponent>
{
    [PunRPC]
    void Shoot()
    {
        GetData().isShoot = true;
    }
}