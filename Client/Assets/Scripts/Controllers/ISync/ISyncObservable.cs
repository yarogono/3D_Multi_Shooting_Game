using Google.Protobuf;

namespace Assets.Scripts.Controllers.Player
{
    interface ISyncObservable
    {

        void OnSync(IMessage packet);
    }
}
    