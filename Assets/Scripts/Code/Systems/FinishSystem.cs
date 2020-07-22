using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(FinishSystem))]
public sealed class FinishSystem : UpdateSystem 
{

    public override void OnAwake() 
    {
    }

    public override void OnUpdate(float deltaTime) 
    {
    }


}