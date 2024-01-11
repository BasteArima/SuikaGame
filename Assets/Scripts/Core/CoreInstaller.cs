using Core.Behaviors;
using Core.Scriptables;
using UnityEngine;
using Zenject;

namespace Core
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private BallsDataPackContainer _ballsDataPackContainer;
        [SerializeField] private PlayerMoveController _playerMoveController;
        [SerializeField] private BallSpawner _ballSpawner;
        [SerializeField] private BallsMerger _ballsMerger;

        public override void InstallBindings()
        {
            Container.Bind<BallsDataPackContainer>().FromInstance(_ballsDataPackContainer).AsSingle().NonLazy();
            Container.Bind<PlayerMoveController>().FromInstance(_playerMoveController).AsSingle().NonLazy();
            Container.Bind<BallSpawner>().FromInstance(_ballSpawner).AsSingle().NonLazy();
            Container.Bind<BallsMerger>().FromInstance(_ballsMerger).AsSingle().NonLazy();
        }
    }
}