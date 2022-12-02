using UI.Impl;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(menuName = "Prefabs/" + nameof(UiPrefabs),
        fileName = nameof(UiPrefabs))]
    public class UiPrefabs : ScriptableObject
    {
        public Canvas mainCanvas;
        [Header("Panels")]
        public MainPanelView mainPanelView;
        public SettingsPanelView settingsPanelView;
        public GamePanelView gamePanelView;
    }
}