using Game.Environment;
using Game.FarmLogic;
using Game.Interaction;
using Installers;
using Zenject;

namespace Game.Player
{
    public class PlayerInitializeSystem
    {
        private readonly EnvironmentPrefabs _environmentPrefabs;
        private readonly GameHolder _gameHolder;
        
        public PlayerInitializeSystem(
            EnvironmentPrefabs environmentPrefabs,
            GameHolder gameHolder
        )
        {
            _environmentPrefabs = environmentPrefabs;
            _gameHolder = gameHolder;
            Initialize();
        }
        
        public void Initialize()
        {
            var player =
                DiContainerRef.Container.InstantiatePrefabForComponent<PlayerView>(_environmentPrefabs.PlayerView);
            DiContainerRef.Container.Bind<PlayerView>().FromInstance(player).AsSingle().NonLazy();
            player.transform.position = _gameHolder.SpawnPointPlayer;
            
            /*
            DiContainerRef.Container.Resolve<PlayerView>();
            DiContainerRef.Container.Resolve<PlayerMoveSystem>();*/
            
        }
    }
}