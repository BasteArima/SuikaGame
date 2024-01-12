using Core.Signals;
using Core.Types;
using DG.Tweening;
using Sound.Enums;
using TMPro;
using UniRx;
using UnityEngine;

namespace Core.Behaviors
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicBall : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private TMP_Text _scoreText;

        private int _score;

        public int Score
        {
            get => _score;
            private set
            {
                if (value < 0)
                    value = 0;
                _score = value;
                _scoreText.text = _score.ToString();
            }
        }

        public Rigidbody2D Rigidbody { get; private set; }

        public BallData BallData { get; private set; }
        public SoundIds MergeSound { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Initialize(BallData ballData)
        {
            BallData = ballData;
            transform.DOScale(ballData.Size, 0);
            Score = ballData.Score;
            _spriteRenderer.color = ballData.Color;
            if (null != ballData.Sprite)
                _spriteRenderer.sprite = ballData.Sprite;
            MergeSound = ballData.MergeSound;
        }

        private void OnCollisionWithOtherPhysicBall(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<PhysicBall>(out var ball))
            {
                if (ball.Score == Score)
                    MessageBroker.Default.Publish(new MergeBallsSignal()
                    {
                        FirstBall = this,
                        SecondBall = ball
                    });
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionWithOtherPhysicBall(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            OnCollisionWithOtherPhysicBall(collision);
        }
    }
}