using System.Collections;
using System.Collections.Generic;
using Enums;
using Game.SceneLoading;
using UnityEngine;

public class SplashManager
{
    private readonly SceneLoadingManager _sceneLoadingManager;
    
    public SplashManager(
        SceneLoadingManager sceneLoadingManager
    )
    {
        _sceneLoadingManager = sceneLoadingManager;
    }
    
    public void Initialize()
    {
        LoadGame();
    }

		
    private void LoadGame()
    {
        _sceneLoadingManager.LoadLocationScene(ELocationType.Game);
    }
}
