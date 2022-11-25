using System;
using UnityEngine;

namespace UI.Impl
{
    public abstract class UiPanel : MonoBehaviour, IUiPanel
    {
        public PanelEnum panelType;

        public event Action<PanelEnum> onPanelOpen;

        public virtual void NavigateTo(PanelEnum menuPanelEnum)
        {
            onPanelOpen?.Invoke(menuPanelEnum);
        }

        public virtual void EnablePanel(bool isEnable)
        {
            gameObject.SetActive(isEnable);
        }
    }
}