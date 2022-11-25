using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Impl
{
    public class SettingsPanelView : UiPanel, IInitializable
    {
        [SerializeField] private Button onMainMenu;
        
        public override void EnablePanel(bool isEnable)
        {
            gameObject.SetActive(isEnable);
        }

        public void Initialize()
        {
            onMainMenu.onClick.AddListener(delegate
            {
                NavigateTo(PanelEnum.MainMenu);
            });
        }
    }
}