using UI.Behaviors;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private InterfaceManager _interfaceManager;
    [SerializeField] private InputController _inputController;
    
    public override void InstallBindings()
    {
        Container.Bind<InterfaceManager>().FromInstance(_interfaceManager).AsSingle().NonLazy();
        Container.Bind<InputController>().FromInstance(_inputController).AsSingle().NonLazy();
    }
}