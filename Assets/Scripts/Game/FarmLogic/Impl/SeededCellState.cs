using System.Collections;
using Enums;
using UniRx;
using UnityEngine;

namespace Game.FarmLogic.Impl
{
    public class SeededCellState : IFarmCellState, IGUIHandler
    {
        private readonly FarmCellView _cellView;
        private readonly CellPlantParameters _plantParameters;
        private readonly EPlantType _plantType;
        public bool IsHandled { get; set; }
        
        public SeededCellState(float timeToRipe, FarmCellView cellView, EPlantType plantType)
        {
            IsHandled = true;
            
            _cellView = cellView;
            _plantParameters = cellView.PlantParameters;
            _plantType = plantType;
                
            Observable.FromCoroutine(() => WaitRipening(timeToRipe))
                .DoOnCompleted(OnRipening)
                .Subscribe();
        }

        public void Handle(EPlantType type)
        {
            Debug.Log("actually seed");
        }

        private IEnumerator WaitRipening(float timeToRipe)
        {
            _cellView.CellGUIView.SwitchGuiEnable(true);
            _cellView.CellGUIView.SwitchPlantPanelEnable(false);
            var time = 0f;
            
            do
            {
                time += Time.deltaTime;
                ShowTimeRipening(timeToRipe - time);
                
                yield return null;
            } while (time < timeToRipe);
        }

        private void OnRipening()
        {
            _cellView.CellGUIView.SwitchGuiEnable(false);
            _cellView.Renderer.material = _plantParameters.GetPlant(_plantType).ripeMaterial;
            _cellView.State = new RipedCellState(_cellView);
        }

        public void ShowTimeRipening(float remainingTime)
        {
            _cellView.CellGUIView.SetTimeText(remainingTime.ToString("0.0"));
        }
    }
}