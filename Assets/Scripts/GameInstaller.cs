using Scriptables;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private BallsData _ballsData;
    [SerializeField] private PlayerMoveController _playerMoveController;
    [SerializeField] private BallSpawner _ballSpawner;
    [SerializeField] private BallsMerger _ballsMerger;
    
    public override void InstallBindings()
    {
        Container.Bind<BallsData>().FromInstance(_ballsData).AsSingle().NonLazy();
        Container.Bind<PlayerMoveController>().FromInstance(_playerMoveController).AsSingle().NonLazy();
        Container.Bind<BallSpawner>().FromInstance(_ballSpawner).AsSingle().NonLazy();
        Container.Bind<BallsMerger>().FromInstance(_ballsMerger).AsSingle().NonLazy();
    }
}