using Db;
using Db.Impl;
using Game.Environment;
using Game.FarmLogic;
using Game.FarmLogic.Impl;
using Game.Interaction;
using Game.Player;
using Game.Player.Equipment;
using Game.Player.Equipment.Impl;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private GameHolder gameHolder;
        [SerializeField] private PlayerView playerViewPrefab;

        [Inject] private EnvironmentPrefabs _environmentPrefabs;
        
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