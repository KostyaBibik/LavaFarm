using Enums;
using UnityEngine;

namespace Game.FarmLogic.Impl
{
    public class RipedCellState : IFarmCellState
    {
        private readonly FarmCellView _cellView;
        private readonly CellPlantParameters _plantParameters;
        public bool IsHandled { get; set; }
        
        public RipedCellState(FarmCellView cellView)
        {
            _cellView = cellView;
            _plantParameters = cellView.PlantParameters;
            IsHandled = true;
        }

        public void Handle(EPlantType type)
        {
            _cellView.State = new EmptyCellState(_cellView).Initialize(_plantParameters);
            _cellView.Renderer.material = _plantParameters.GetPlant(type).grassMaterial;
        }
    }
}