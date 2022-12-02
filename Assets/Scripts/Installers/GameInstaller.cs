using Db;
using Db.Impl;
using Game.Environment;
using Game.FarmLogic;
using Game.FarmLogic.Impl;
using Game.Interaction;
using Game.Player;
using Game.Player.Equipment.Impl;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private GameHolder gameHolder;

        [Inject] private EnvironmentPrefabs _environmentPrefabs;
        
        public override void InstallBindings()
        {
            InstallFarm();
            
            InstallEnvironment();

            InstallGameHolder();

            InstallPlayer();
            
            InstallInput();

            InstallCellFactory();

            InstallPrefsSystem();

            InstallProgressSystem();
        }

        private void InstallGameHolder()
        {
            Container
                .Bind<GameHolder>()
                .FromInstance(gameHolder)
                .AsSingle();
        }
        
        private void InstallFarm()
        {
            Container
                .Bind<FarmInitializeSystem>()
                .FromNewComponentOnNewGameObject()
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
            var meshSurfaceView = Container.InstantiatePrefabForComponent<NavMeshSurface>(
                _environmentPrefabs.Ground,
                gameHolder.SpawnPointGround,
                Quaternion.identity,
                null
            );
            
            Container
                .Bind<NavMeshSurface>()
                .FromInstance(meshSurfaceView)
                .AsSingle()
                .NonLazy();
        }
        
        private void InstallPlayer()
        {
            var playerView = Container.InstantiatePrefabForComponent<PlayerView>(
                    _environmentPrefabs.PlayerView,
                    gameHolder.SpawnPointPlayer,
                    Quaternion.identity,
                    null
                );
            
            Container
                .Bind<PlayerView>()
                .FromInstance(playerView)
                .AsSingle()
                .NonLazy();
            
            CreatePlayerEquipment(playerView);

            Container
                .Bind<PlayerHandlingSystem>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<PlayerMoveSystem>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallCellFactory()
        {
            Container
                .BindInterfacesAndSelfTo<FarmCellFactory>()
                .AsSingle()
                .Lazy();
        }

        private void InstallPrefsSystem()
        {
            Container
                .BindInterfacesAndSelfTo<PrefsManager>()
                .AsSingle()
                .NonLazy();
        }
        
        private void InstallProgressSystem()
        {
            Container
                .Bind(typeof(IPlayerProgressSystem), typeof(IInitializable))
                .To<PlayerProgressSystem>()
                .AsSingle()
                .NonLazy();
        }

        private void CreatePlayerEquipment(PlayerView playerView)
        {
            var scytheView = Container
                .InstantiatePrefabForComponent<ScytheEquipmentView>(_environmentPrefabs.Scythe);
            Container.Bind<ScytheEquipmentView>().FromInstance(scytheView);
            
            scytheView.transform.SetParent(playerView.ScytheHolder, false);
            scytheView.gameObject.SetActive(false);
            
            var axeView = Container
                .InstantiatePrefabForComponent<AxeEquipmentView>(_environmentPrefabs.Axe);
            Container.Bind<AxeEquipmentView>().FromInstance(axeView);
            
            axeView.transform.SetParent(playerView.AxeHolder, false);
            axeView.gameObject.SetActive(false);
        }
        
        public void Initialize()
        {
            Container.Resolve<IFarmCellFactory>();
        }
    }
}