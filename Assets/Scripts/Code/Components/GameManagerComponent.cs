using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct GameManagerComponent : IComponent {
    public Transform[] startPointPlayers;
    public Transform[] startPointEmblem;
    public GameObject playerPrefab;
    public GameObject emblemPrefab;
}