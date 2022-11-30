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
            player.transform.position = _gameHolder.SpawnPointGround;
            //DiContainerRef.Container.Bind<PlayerMoveSystem>().AsSingle().WithArguments(player).NonLazy();
        }
    }
}