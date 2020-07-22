using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Photon.Pun;
using Morpeh.Globals;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DestroySystem))]
public sealed class DestroySystem : UpdateSystem 
{
    private Filter filterDestroyPhotonAnimation;
    private Filter filterDestroyPhoton;

    public override void OnAwake() 
    {
        filterDestroyPhotonAnimation = World.Filter.With<HealthComponent>().With<PhotonComponent>().With<AnimationTankComponent>();
        filterDestroyPhoton = World.Filter.With<HealthComponent>().With<PhotonComponent>();
    }

    public override void OnUpdate(float deltaTime) 
    {
        ref var windowFinish = ref World.Filter.With<FinishWindowComponent>().First().GetComponent<FinishWindowComponent>();

        foreach (var entity in filterDestroyPhotonAnimation)
        {
            ref var health = ref entity.GetComponent<HealthComponent>();
            ref var photon = ref entity.GetComponent<PhotonComponent>();
            ref var animation = ref entity.GetComponent<AnimationTankComponent>();


            if (health.healthPoints <= 0 && photon.photonView.IsMine)
            {
                animation.animator.SetTrigger("TriggerDestroy");
                windowFinish.ui.SetActive(true);
                windowFinish.text.text = "Lose";
            }
            else if (health.healthPoints <= 0 && !windowFinish.ui.activeSelf)
            {
                windowFinish.ui.SetActive(true);
                windowFinish.text.text = "Win";
            }
        }

        foreach (var entity in filterDestroyPhoton)
        {
            ref var health = ref entity.GetComponent<HealthComponent>();
            ref var photon = ref entity.GetComponent<PhotonComponent>();


            if (health.healthPoints <= 0 && photon.photonView.IsMine)
            {
                PhotonNetwork.Destroy(health.gameObject);
                windowFinish.ui.SetActive(true);
                windowFinish.text.text = "Lose";
            }
            else if (health.healthPoints <= 0 && !windowFinish.ui.activeSelf)
            {
                windowFinish.ui.SetActive(true);
                windowFinish.text.text = "Win";
            }
        }
    }
}