using Db;
using UI;
using UI.Impl;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private PanelEnum startPanelType;

        [SerializeField] private UiPrefabs uiPrefabs;

        public override void InstallBindings()
        {
            var canvas = Container.InstantiatePrefabForComponent<Canvas>(uiPrefabs.mainCanvas);

            InstallPanels(canvas.transform);

            InstallPanelHandler();
        }

        private void InstallPanels(Transform canvas)
        {
            Container
                .BindInterfacesAndSelfTo<MainPanelView>()
                .FromComponentInNewPrefab(uiPrefabs.mainPanelView)
                .UnderTransform(canvas)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<GamePanelView>()
                .FromComponentInNewPrefab(uiPrefabs.gamePanelView)
                .UnderTransform(canvas)
                .AsSingle(); 
        }

        private void InstallPanelHandler()
        {
            Container
                .Bind<PanelsHandler>()
                .AsSingle()
                .WithArguments(startPanelType)
                .NonLazy();
        }
    }
}