using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Game.SceneLoading;
using UnityEngine;
using Zenject;

public class SplashManager : IInitializable
{
    private readonly SceneLoadingManager _sceneLoadingManager;
    
    public SplashManager(
        SceneLoadingManager sceneLoadingManager
    )
    {
        Debug.Log("SplashManager");
        _sceneLoadingManager = sceneLoadingManager;
    }
    
    public void Initialize()
    {
        LoadGame();
    }

		
    private void LoadGame()
    {
        _sceneLoadingManager.LoadLocationScene(ELocationType.Game, 10f);
    }
}
