using System.Collections;
using Enums;
using Installers;
using UniRx;
using UnityEngine;

namespace Game.FarmLogic.Impl
{
    public class SeededCellState : IFarmCellState, IGUIHandler
    {
        public bool IsObstacle { get; set; }
        public bool IsHandled { get; set; }
        public bool IsRiped { get; set; }

        private readonly FarmCellView _cellView;
        private readonly CellPlantParameters _plantParameters;
        private readonly EPlantType _plantType;
        private PlantView _plantedObjView;

        public SeededCellState(float timeToRipe, FarmCellView cellView, EPlantType plantType)
        {
            _cellView = cellView;
            _plantParameters = cellView.PlantParameters;
            _plantType = plantType;
            IsHandled = true;
            IsRiped = false;
            IsObstacle = true;
                        
            _plantedObjView = DiContainerRef.Container.InstantiatePrefabForComponent<PlantView>(
                _plantParameters.GetPlant(plantType).plantView
            );
            
            _plantedObjView.transform.position = _cellView.posToSpawnPlant.position;
            _cellView.CellGUIView.SwitchTimeLabelEnable(true);
            
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
            var startScale = _plantedObjView.transform.localScale;
            var targetScale = _plantParameters.GetPlant(_plantType).endGrowScale;
            var startPos = _plantedObjView.transform.position;
            var onEndGrowHeightPos = _plantParameters.GetPlant(_plantType).onEndGrowHeightPos + startPos.y;
            
            do
            {
                time += Time.deltaTime;
                var ratioGrowing = time / timeToRipe;
                _plantedObjView.transform.localScale = Vector3.Lerp(startScale, targetScale, ratioGrowing);
                _plantedObjView.transform.position = new Vector3(
                    startPos.x,
                    Mathf.Lerp(startPos.y, onEndGrowHeightPos, ratioGrowing),
                    startPos.z);
                
                ShowTimeRipening(timeToRipe - time);
                
                yield return null;
            } while (time < timeToRipe);
        }

        private void OnRipening()
        {
            _cellView.CellGUIView.SwitchGuiEnable(false);
            _cellView.CellGUIView.SwitchTimeLabelEnable(false);
            
            _cellView.Renderer.material = _plantParameters.RipedBlock;
            _cellView.State = new RipedCellState(_cellView, _plantedObjView);
            _plantedObjView = null;
        }

        public void ShowTimeRipening(float remainingTime)
        {
            _cellView.CellGUIView.SetTimeText(remainingTime.ToString("0.0"));
        }
    }
}