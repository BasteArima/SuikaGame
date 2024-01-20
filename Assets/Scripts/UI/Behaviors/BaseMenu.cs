using UI.Enums;
using UnityEngine;

namespace UI.Behaviors
{
    public abstract class BaseMenu : MonoBehaviour
    {
        public abstract MenuName Name { get; }

        private Canvas _canvas;

        public virtual void SetState(bool state)
        {
            if (null != _canvas)
            {
                _canvas.gameObject.SetActive(state);
            }
            
            gameObject.SetActive(state);
        }

        public virtual void Init()
        {
            transform.parent.TryGetComponent(out _canvas);
        }
        
    }
}