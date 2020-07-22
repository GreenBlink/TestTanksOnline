using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using System.Linq;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(BulletMoveSystem))]
public sealed class BulletMoveSystem : UpdateSystem 
{
    private Filter filterBullets;

    public override void OnAwake()
    {
        filterBullets = World.Filter.With<MoveComponent>().With<BulletComponent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in filterBullets)
        {
            ref var moveComponent = ref entity.GetComponent<MoveComponent>();
            Move(moveComponent);
            entity.RemoveComponent<MoveComponent>();
        }
    }

    public void Move(MoveComponent move)
    {
        move.rigidbody2.AddForce(move.transform.up * move.speed, ForceMode2D.Force);       
        //move.transform.Translate(GetMoveInput() * move.speed * deltaTime);
    }
}