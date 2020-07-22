using Morpeh;
using Photon.Pun;
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class BulletProvider : MonoProvider<BulletComponent> 
{
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        Destroy(gameObject);
    } 
}