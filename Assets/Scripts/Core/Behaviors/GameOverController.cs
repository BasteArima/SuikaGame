using UniRx;
using UnityEngine;

namespace Core.Behaviors
{
    public class GameOverController : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<PhysicBall>(out var ball))
            {
                MessageBroker.Default.Publish(new GameOverSignal());
            }
        }
    }
}