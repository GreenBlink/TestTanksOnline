using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Photon.Pun;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerShootSystem))]
public sealed class PlayerShootSystem : UpdateSystem 
{
    private Filter filterPlayer;
    private float currentReloading;

    public override void OnAwake() 
    {
        filterPlayer = World.Filter.With<ShootComponent>().With<PlayerComponent>();
    }

    public override void OnUpdate(float deltaTime) 
    {
        Shoot();
        ReloadingWeapon();
    }

    private void Shoot()
    {
        foreach(var entity in filterPlayer)
        {
            ref var shootComponent = ref entity.GetComponent<ShootComponent>();
            ref var photonComponent = ref entity.GetComponent<PhotonComponent>();

            if (photonComponent.photonView.IsMine && Input.GetKeyDown(KeyCode.F) && currentReloading <= 0)
            {
                shootComponent.isShoot = true;
                photonComponent.photonView.RPC("Shoot", RpcTarget.Others);
            }

            if (shootComponent.isShoot)
            {
                Instantiate(shootComponent.bullet, shootComponent.shootTransform.position + shootComponent.shootTransform.up / 2, shootComponent.shootTransform.rotation);
                currentReloading = shootComponent.reloadingWeapon;
                shootComponent.isShoot = false;
            }
        }
    }

    private void ReloadingWeapon()
    {
        if (currentReloading > 0)
            currentReloading -= Time.deltaTime;
    }
}