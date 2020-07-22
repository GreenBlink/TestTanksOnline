using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Photon.Pun.Demo.PunBasics;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(TeleportSystem))]
public sealed class TeleportSystem : UpdateSystem 
{
    private Filter filterTeleports;
    public override void OnAwake()
    {
        filterTeleports = World.Filter.With<TeleportComponent>().With<HealthComponent>();

        foreach (var entity in filterTeleports)
        {
            ref var teleportComponent = ref entity.GetComponent<TeleportComponent>();
            ref var healthComponent = ref entity.GetComponent<HealthComponent>();
            healthComponent.damageEvent.AddListener(() => Teleport(entity.GetComponent<TeleportComponent>()));
        }
    }

    public override void OnUpdate(float deltaTime) 
    {
        
    }

    public void Teleport(TeleportComponent teleportComponent)
    {
        if (teleportComponent.targets.Count == 0)
            return;

        Transform randomTarget = teleportComponent.targets[Random.Range(0, teleportComponent.targets.Count)];
        teleportComponent.transformObject.position = randomTarget.position;
        teleportComponent.transformObject.rotation = randomTarget.rotation;
    }
}