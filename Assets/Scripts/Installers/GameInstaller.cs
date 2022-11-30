using Db;
using Db.Impl;
using Game;
using Game.Environment;
using Game.FarmLogic;
using Game.FarmLogic.Impl;
using Game.Interaction;
using Game.Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private GameHolder gameHolder;
        [SerializeField] private PlayerView playerViewPrefab;
        
        public override void InstallBindings()
        {
            Container
                    .Bind<FarmInitializeSystem>()
                    .FromNewComponentOnNewGameObject()
                    .AsSingle()
                    .NonLazy();
            
            Container
                .Bind<GameHolder>()
                .FromInstance(gameHolder)
                .AsSingle();

            InstallEnvironment();
            
            InstallPlayer();
            
            InstallInput();
            
            Container
                .BindInterfacesAndSelfTo<FarmCellFactory>()
                .AsSingle()
                .Lazy();

            Container
                .BindInterfacesAndSelfTo<PrefsManager>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind(typeof(IPlayerProgressSystem), typeof(IInitializable))
                .To<PlayerProgressSystem>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallInput()
        {
            Container
                .Bind(typeof(ITickable), typeof(IInitializable))
                .To<InputUpdateSystem>()
                .AsSingle()
                .NonLazy();
        }
        
        private void InstallEnvironment()
        {
            Container
                .Bind(typeof(IInitializable))
                .To<EnvironmentInitializeSystem>()
                .AsSingle()
                .NonLazy()
                ;
        }
        
        private void InstallPlayer()
        {
            var playerView = Container.InstantiatePrefabForComponent<PlayerView>(
                    playerViewPrefab,
                    gameHolder.SpawnPointPlayer,
                    Quaternion.identity,
                    null
                );
            
            Container
                .Bind<PlayerView>()
                .FromInstance(playerView)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<PlayerHandlingSystem>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<PlayerMoveSystem>()
                .AsSingle()
                .NonLazy();
        }

        public void Initialize()
        {
            Container.Resolve<IFarmCellFactory>();
        }
    }
}