using Enums;

namespace Game.FarmLogic.Impl
{
    public class EmptyCellState : IFarmCellState
    {
        private readonly FarmCellView _cellView;
        private CellPlantParameters _plantParameters;
        public bool IsHandled { get; set; }
        public bool IsRiped { get; set; }

        public EmptyCellState(FarmCellView cellView)
        {
            _cellView = cellView;
            IsHandled = false;
            IsRiped = false;
        }

        public void Handle(EPlantType type)
        {
            _cellView.State = new SeededCellState(_plantParameters.GetPlant(type).timeToRipening, _cellView, type);
            _cellView.Renderer.material = _plantParameters.GetPlant(type).grassMaterial;
        }
        
        public IFarmCellState Initialize(CellPlantParameters plantParameters)
        {
            _plantParameters = plantParameters;

            return this;
        }
    }
}