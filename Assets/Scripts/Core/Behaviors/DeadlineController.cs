using Core.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Behaviors
{
    public class DeadlineController : MonoBehaviour
    {
        [Inject]
        protected virtual void Construct()
        {
            MessageBroker.Default
                .Receive<DeadlineCrossSignal>()
                .Subscribe(OnDeadlineCross)
                .AddTo(gameObject);
        }

        private void OnDeadlineCross(DeadlineCrossSignal deadlineCrossSignal)
        {
            MessageBroker.Default.Publish(new GameOverSignal());
        }
    }
}