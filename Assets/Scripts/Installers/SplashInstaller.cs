using UnityEngine;
using Zenject;

public class SplashInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("InstallBindings");
        Container.BindInterfacesAndSelfTo<SplashManager>().AsSingle().NonLazy();
    }
}