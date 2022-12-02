using Db.Impl;
using Game.SceneLoading;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<SceneLoadingManager>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<PrefsManager>()
                .AsSingle()
                .NonLazy();
            
        }
    }
}