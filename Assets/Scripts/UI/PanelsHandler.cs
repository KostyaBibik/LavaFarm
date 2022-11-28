using System.Collections.Generic;
using UI.Impl;
using UnityEngine;
using Zenject;

namespace UI
{
    public class PanelsHandler
    {
        private List<UiPanel> _uiPanels = new List<UiPanel>();

        private readonly MainPanelView _mainPanelView;
        
        public PanelsHandler(
            MainPanelView mainPanelView,
            SettingsPanelView settingsPanelView,
            PanelEnum panelType
            )
        {
            Debug.Log($"panelType: {panelType}");
            _uiPanels.Add(mainPanelView);
            _uiPanels.Add(settingsPanelView);

            SubscribeToPanels();
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
                Debug.Log($"panel: {panel.panelType}");
                panel.EnablePanel(panel.panelType == panelType);
            }
        }
    }
}