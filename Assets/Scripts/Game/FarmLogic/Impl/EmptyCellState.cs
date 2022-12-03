using Enums;

namespace Game.FarmLogic.Impl
{
    public class EmptyCellState : IFarmCellState
    {
        private readonly FarmCellView _cellView;
        private CellPlantParameters _plantParameters;
        public bool IsObstacle { get; set; }
        public bool IsHandled { get; set; }
        public bool IsRiped { get; set; }

        public EmptyCellState(FarmCellView cellView)
        {
            _cellView = cellView;
            IsHandled = false;
            IsRiped = false;
            IsObstacle = false;
        }

        public void Handle(EPlantType type)
        {
            _cellView.State = new SeededCellState(_plantParameters.GetPlant(type).timeToRipening, _cellView, type)
                {
                    IsObstacle = false
                };
            _cellView.Renderer.material = _plantParameters.PlantedBlock;
        }
        
        public IFarmCellState Initialize(CellPlantParameters plantParameters)
        {
            _plantParameters = plantParameters;

            return this;
        }
    }
}