using UnityEngine;
using Zenject;

public class SplashInstaller : MonoInstaller
{
    // ReSharper disable Unity.PerformanceAnalysis
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SplashManager>().AsSingle().NonLazy();
    }
}