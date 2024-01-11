using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Core.Behaviors
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicBall : MonoBehaviour
    {
        public Action<PhysicBall, PhysicBall> MergeReady;

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

        public float Size { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Initialize(float size, int score, Color color, Sprite sprite)
        {
            Size = size;
            transform.DOScale(Size, 0);
            Score = score;
            _spriteRenderer.color = color;
            if (null != sprite)
                _spriteRenderer.sprite = sprite;

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<PhysicBall>(out var ball))
            {
                if (ball.Score == Score)
                    MergeReady?.Invoke(this, ball);
            }
        }
    }
}