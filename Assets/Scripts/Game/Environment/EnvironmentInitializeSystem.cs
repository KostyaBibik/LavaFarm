using Game.FarmLogic;
using Installers;
using Zenject;

namespace Game.Environment
{
    public class EnvironmentInitializeSystem : IInitializable
    {
        private readonly EnvironmentPrefabs _environmentPrefabs;
        private readonly GameHolder _gameHolder;
        
        public EnvironmentInitializeSystem(
            EnvironmentPrefabs environmentPrefabs,
            GameHolder gameHolder
            )
        {
            _environmentPrefabs = environmentPrefabs;
            _gameHolder = gameHolder;
        }
        
        public void Initialize()
        {
            var ground = DiContainerRef.Container.InstantiatePrefab(_environmentPrefabs.Ground);
            ground.transform.position = _gameHolder.SpawnPointGround;
        }
    }
}