using System;

namespace UI
{
    public interface IUiPanel
    {
        public event Action<PanelEnum> onPanelOpen;
        
        void NavigateTo(PanelEnum menuPanelEnum);

        void EnablePanel(bool isEnable);
    }
}