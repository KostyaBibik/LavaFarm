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

            Container
                .BindInterfacesAndSelfTo<MainPanelView>()
                .FromComponentInNewPrefab(uiPrefabs.mainPanelView)
                .UnderTransform(canvas.transform)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<SettingsPanelView>()
                .FromComponentInNewPrefab(uiPrefabs.settingsPanelView)
                .UnderTransform(canvas.transform)
                .AsSingle();
                //.OnInstantiated((context, o) => ((MonoBehaviour) o).gameObject.SetActive(false));

             Container
                .BindInterfacesAndSelfTo<GamePanelView>()
                .FromComponentInNewPrefab(uiPrefabs.gamePanelView)
                .UnderTransform(canvas.transform)
                .AsSingle();   
                
            Container
                .Bind<PanelsHandler>()
                .AsSingle()
                .WithArguments(startPanelType)
                .NonLazy();
        }
    }
}