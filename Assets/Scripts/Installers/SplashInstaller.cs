using Game.SceneLoading;
using Zenject;

namespace Installers
{
    public class SplashInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SplashManager>().AsSingle().NonLazy();
        }
    }
}