using Enums;
using UnityEngine;
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
            LoadGame();
        }

        private void LoadGame()
        {
            _sceneLoadingManager.LoadLocationScene(ELocationType.Game, 2f);
        }
    }
}
