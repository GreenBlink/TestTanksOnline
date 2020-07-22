using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Photon.Pun;
using System.Collections.Generic;
using Morpeh.Globals;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(GameManagerSystem))]
public sealed class GameManagerSystem : UpdateSystem 
{
    private Filter filterGameManager;

    public GlobalEvent LeavePlayerEvent;

    public override void OnAwake() 
    {
        filterGameManager = World.Filter.With<GameManagerComponent>();
        InitPlayer();
    }

    public override void OnUpdate(float deltaTime) 
    {
        LeavePlayer();
    }

    private void InitPlayer()
    {
        foreach (var entity in filterGameManager)
        {
            ref var gameManagerComponent = ref entity.GetComponent<GameManagerComponent>();
            Transform startPosition = gameManagerComponent.startPointPlayers[PhotonNetwork.LocalPlayer.ActorNumber - 1];
            TeleportProvider teleport = PhotonNetwork.Instantiate(gameManagerComponent.playerPrefab.name, startPosition.position, startPosition.rotation, 0).GetComponent<TeleportProvider>();
            PhotonNetwork.Instantiate(gameManagerComponent.emblemPrefab.name, gameManagerComponent.startPointEmblem[PhotonNetwork.LocalPlayer.ActorNumber - 1].position, gameManagerComponent.startPointEmblem[PhotonNetwork.LocalPlayer.ActorNumber - 1].rotation, 0);


            if (teleport != null)
                teleport.GetData().targets = new List<Transform> { startPosition };
        }
    }

    private void LeavePlayer()
    {
        if (LeavePlayerEvent.IsPublished)
        {
            PhotonNetwork.Disconnect();
        }
    }
}