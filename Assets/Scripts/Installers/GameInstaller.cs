using Game.FarmLogic;
using Game.FarmLogic.Impl;
using Game.Interaction;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private GameHolder gameHolder;
        
        public override void InstallBindings()
        {
            Container
                    .Bind<FarmInitializeSystem>()
                    .FromNewComponentOnNewGameObject()
                    .AsSingle()
                    .NonLazy();
            
            Container
                .Bind<GameHolder>()
                .FromInstance(gameHolder)
                .AsSingle();
            
            Container
                .Bind(typeof(ITickable), typeof(IInitializable))
                .To<InputUpdateSystem>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<FarmCellFactory>()
                .AsSingle()
                .Lazy();
        }

        public void Initialize()
        {
            Container.Resolve<IFarmCellFactory>();
        }
    }
}