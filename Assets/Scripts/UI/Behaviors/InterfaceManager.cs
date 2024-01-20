using UI.Enums;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Behaviors
{
    public class InterfaceManager : MonoBehaviour
    {
        [SerializeField] private BaseMenu[] _menus;

        public ReactiveProperty<MenuName> CurrentTab = new ReactiveProperty<MenuName>();
        
        private void Awake()
        {
            MessageBroker.Default
                .Receive<StartGameSignal>()
                .Subscribe(OnRestarted)
                .AddTo(gameObject);
            CurrentTab.Subscribe(OnChangeCurrentTab);
            InitMenus();
        }

        private void Start()
        {
            CurrentTab.Value = MenuName.CoreGame;
        }

        private void InitMenus()
        {
            for (int i = 0; i < _menus.Length; i++)
                _menus[i].Init();
        }

        private void OnRestarted(StartGameSignal startGameSignal)
        {
            CurrentTab.Value = MenuName.CoreGame;
        }

        private void OnChangeCurrentTab(MenuName tab)
        {
            Toggle(tab);
        }

        private void Toggle(MenuName name)
        {
            foreach (var baseMenu in _menus)
            {
                bool state = baseMenu.Name == name;
                baseMenu.SetState(state);
            }
        }
    } 
}