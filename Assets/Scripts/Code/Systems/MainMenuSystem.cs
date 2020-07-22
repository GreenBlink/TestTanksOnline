using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Morpeh.Globals;
using UnityEngine.SceneManagement;
using Photon.Pun;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MainMenuSystem))]
public sealed class MainMenuSystem : UpdateSystem {
    public GlobalEvent FindRoomEvent;
    public GlobalEvent StartSoloGameEvent;
    public GlobalEvent StopFindRoomEvent;

    public override void OnAwake() {
    }

    public override void OnUpdate(float deltaTime) 
    {
        FindRoom();
        StartSoloGame();
        StopFindRoom();
    }

    private void FindRoom()
    {
        if (FindRoomEvent.IsPublished)
        {
            NetworkManager.instance.Connect();
        }
    }

    private void StopFindRoom()
    {
        if (StopFindRoomEvent.IsPublished)
        {
            PhotonNetwork.Disconnect();
        }
    }

    private void StartSoloGame()
    {
        if (StartSoloGameEvent.IsPublished)
        {
            NetworkManager.instance.ConnectOffline();
        }
    }
}