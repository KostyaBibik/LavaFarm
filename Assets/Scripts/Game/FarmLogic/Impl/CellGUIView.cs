using System;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.FarmLogic.Impl
{
    public class CellGUIView : MonoBehaviour
    {
        [Header("Text labels")]
        [SerializeField] private TMP_Text timeLabel;

        [Header("Choose plant buttons")] 
        [SerializeField] private Button carrotBtn;
        [SerializeField] private Button grassBtn;
        [SerializeField] private Button treeBtn;

        [Header("Panels")] 
        [SerializeField] private GameObject choosePlantPanel;
        
        public event Action<EPlantType> onChooseBtn;
        
        public void SwitchGuiEnable(bool flag)
        {
            gameObject.SetActive(flag);
        }

        public void SetTimeText(string text)
        {
            timeLabel.text = text;
        }

        public void SwitchTimeLabelEnable(bool flag)
        {
            timeLabel.gameObject.SetActive(flag);
        }
        
        public void SwitchPlantPanelEnable(bool flag)
        {
            if (flag)
                SwitchGuiEnable(true);
            
            choosePlantPanel.SetActive(flag);
        }

        public CellGUIView InitializeBtns()
        {
            carrotBtn.onClick.AddListener(delegate
            {
                onChooseBtn?.Invoke(EPlantType.Carrot);
            });
            
            grassBtn.onClick.AddListener(delegate
            {
                onChooseBtn?.Invoke(EPlantType.Grass);
            });
            
            treeBtn.onClick.AddListener(delegate
            {
                onChooseBtn?.Invoke(EPlantType.Tree);
            });

            return this;
        }
    }
}