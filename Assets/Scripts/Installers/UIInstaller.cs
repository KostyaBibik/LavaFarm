using UI;
using UI.Impl;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private PanelEnum startPanelType;
        
        [SerializeField] private MainPanelView mainPanelView;
        [SerializeField] private SettingsPanelView settingsPanelView;
        [SerializeField] private Canvas mainCanvas;

        public override void InstallBindings()
        {
            var canvas = Container.InstantiatePrefabForComponent<Canvas>(mainCanvas);

            Container
                .BindInterfacesAndSelfTo<MainPanelView>()
                .FromComponentInNewPrefab(mainPanelView)
                .UnderTransform(canvas.transform)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<SettingsPanelView>()
                .FromComponentInNewPrefab(settingsPanelView)
                .UnderTransform(canvas.transform)
                .AsSingle()
                .OnInstantiated((context, o) => ((MonoBehaviour) o).gameObject.SetActive(false));;

            Container.Bind<PanelsHandler>().AsSingle().WithArguments(startPanelType).NonLazy();
        }
    }
}