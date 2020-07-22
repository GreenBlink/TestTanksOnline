using Morpeh;
using Photon.Pun;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class HealthProvider : MonoProvider<HealthComponent>, IPunObservable
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
         ref HealthComponent health = ref GetData();

        if (collision.tag == "Bullet")
        {
            health.healthPoints--;

            if (health.healthPoints > 0)
                health.damageEvent.Invoke();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(GetData().healthPoints);
        }
        else
        {
            GetData().healthPoints = (int)stream.ReceiveNext();
        }
    }
}