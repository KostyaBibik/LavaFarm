using TMPro;
using UnityEngine;

namespace Game.FarmLogic.Impl
{
    public class CellGUIView : MonoBehaviour
    {
        [SerializeField] private TMP_Text timeLabel;

        public TMP_Text TimeLabel => timeLabel;

        public void SwitchGuiEnable(bool flag)
        {
            gameObject.SetActive(flag);
        }
    }
}