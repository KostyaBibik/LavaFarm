using Game.FarmLogic;
using Game.Interaction;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameHolder gameHolder;
        
        public override void InstallBindings()
        {
            Container
                    .Bind<FarmInitializeSystem>()
                    .FromNewComponentOnNewGameObject()
                    .AsSingle()
                    .NonLazy()
                ;
            Container.Bind<GameHolder>().FromInstance(gameHolder).AsSingle();
            
            Container.
                Bind(typeof(ITickable), typeof(IInitializable))
                .To<InputUpdateSystem>()
                .AsSingle()
                .NonLazy();
        }
    }
}