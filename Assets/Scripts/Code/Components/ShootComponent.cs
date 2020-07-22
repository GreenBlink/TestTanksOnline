using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Photon.Pun;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct ShootComponent : IComponent
{
    public Transform shootTransform;
    public BulletProvider bullet;
    public float reloadingWeapon;
    public bool isShoot;
}