using Game.FarmLogic;
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
        }
    }
}