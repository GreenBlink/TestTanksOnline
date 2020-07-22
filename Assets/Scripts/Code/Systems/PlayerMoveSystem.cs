using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Photon.Pun;
using System.Linq;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerMoveSystem))]
public sealed class PlayerMoveSystem : UpdateSystem {
    private Filter filterPlayer;

    public override void OnAwake() 
    {
        filterPlayer = World.Filter.With<MoveComponent>().With<PlayerComponent>().With<AnimationTankComponent>();
    }

    public override void OnUpdate(float deltaTime) 
    {
        foreach (var entity in filterPlayer)
        {
            ref var moveComponent = ref entity.GetComponent<MoveComponent>();
            ref var photonComponent = ref entity.GetComponent<PhotonComponent>();

            if (photonComponent.photonView.IsMine == false && PhotonNetwork.IsConnected == true)
                return;

            ref var animationComponent = ref entity.GetComponent<AnimationTankComponent>();
            Move(moveComponent, animationComponent, deltaTime);
        }
    }
                                      
    public void Move(MoveComponent move, AnimationTankComponent animationComponent, float deltaTime)
    {
        Vector3 vectorMove = GetMoveInput();
        move.rigidbody2.AddForce(vectorMove * move.speed * deltaTime, ForceMode2D.Impulse);

        if (vectorMove != Vector3.zero)
        {
            move.transform.rotation = Quaternion.Euler(GetAngleRotation(vectorMove));
            animationComponent.animator.SetFloat("SpeedMove", 1);
        }
        else
        {
            animationComponent.animator.SetFloat("SpeedMove", 0);
        }

        //move.transform.Translate(GetMoveInput() * move.speed * deltaTime);
    }

    private Vector3 GetAngleRotation(Vector3 vectorMove)
    {
        if (vectorMove.x > 0)
            return new Vector3(0, 0, 270);
        else if (vectorMove.x < 0)
            return new Vector3(0, 0, 90);
        else if (vectorMove.y > 0)
            return new Vector3(0, 0, 0);
        else 
            return new Vector3(0, 0, 180);
    }

    private Vector3 GetMoveInput()
    {
        if (Input.GetAxis("Horizontal") != 0)
            return Vector3.right * Input.GetAxis("Horizontal");
        else if (Input.GetAxis("Vertical") != 0)
            return Vector3.up * Input.GetAxis("Vertical");
        else
            return Vector3.zero;
    }
}