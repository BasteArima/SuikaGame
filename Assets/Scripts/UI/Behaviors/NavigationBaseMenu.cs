using System;
using Zenject;

namespace UI.Behaviors
{
    public abstract class NavigationBaseMenu : BaseMenu
    {
        [Inject] private InputController _inputController;

        protected abstract Action DoWhenPressEscape { get; }

        protected virtual void OnEnable()
        {
            _inputController.EscapePressed += DoTargetAction;
        }

        protected virtual void OnDisable()
        {
            _inputController.EscapePressed -= DoTargetAction;
        }

        private void DoTargetAction()
        {
            DoWhenPressEscape.Invoke();
        }
    }
}