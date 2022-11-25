using Enums;
using Zenject;

namespace Game.SceneLoading
{
    public class SplashManager : IInitializable
    {
        private readonly SceneLoadingManager _sceneLoadingManager;
    
        public SplashManager(SceneLoadingManager sceneLoadingManager)
        {
            _sceneLoadingManager = sceneLoadingManager;
        }
    
        public void Initialize()
        {
            PreLoadGame();
        }

        private void PreLoadGame()
        {
            _sceneLoadingManager.PreLoadLocationScene(ELocationType.Game);
        }
    }
}
