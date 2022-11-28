using Game.SceneLoading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Impl
{
    public class MainPanelView : UiPanel, IInitializable
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button menuButton;
        
        [Inject] private readonly SceneLoadingManager _loadingManager;

        public override void EnablePanel(bool isEnable)
        {
            gameObject.SetActive(isEnable);
        }

        public void Initialize()
        {
            playButton.onClick.AddListener(delegate
            {
                _loadingManager.StartLoadedScene();
            });
            
            menuButton.onClick.AddListener(delegate
            {
                NavigateTo(PanelEnum.Settings);
            });
        }
    }
}