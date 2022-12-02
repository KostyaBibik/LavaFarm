using System.Collections.Generic;
using UI.Impl;

namespace UI
{
    public class PanelsHandler
    {
        private readonly List<UiPanel> _uiPanels = new List<UiPanel>();

        private readonly MainPanelView _mainPanelView;
        
        public PanelsHandler(
            MainPanelView mainPanelView,
            SettingsPanelView settingsPanelView,
            GamePanelView gamePanelView,
            PanelEnum panelType
            )
        {
            _uiPanels.Add(mainPanelView);
            _uiPanels.Add(settingsPanelView);
            _uiPanels.Add(gamePanelView);

            SubscribeToPanels();
            EnablePanel(panelType);
        }

        private void SubscribeToPanels()
        {
            foreach (var panel in _uiPanels)
            {
                panel.onPanelOpen += EnablePanel;
            }
        }
        
        private void EnablePanel(PanelEnum panelType)
        {
            foreach (var panel in _uiPanels)
            {
                panel.EnablePanel(panel.panelType == panelType);
            }
        }
    }
}