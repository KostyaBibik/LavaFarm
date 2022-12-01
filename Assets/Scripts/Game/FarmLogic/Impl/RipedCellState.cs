using Db;
using Enums;

namespace Game.FarmLogic.Impl
{
    public class RipedCellState : IFarmCellState
    {
        private readonly FarmCellView _cellView;
        private readonly CellPlantParameters _plantParameters;
        private readonly IPrefsManager _prefsManager;
        private readonly PlantView _plantView;
        public bool IsHandled { get; set; }
        public bool IsRiped { get; set; }

        public RipedCellState(FarmCellView cellView, PlantView plantView)
        {
            _cellView = cellView;
            _plantParameters = cellView.PlantParameters;
            _prefsManager = cellView.PrefsManager;
            _plantView = plantView;
            IsHandled = true;
            IsRiped = true;
        }

        public void Handle(EPlantType type)
        {
            var plant = _plantParameters.GetPlant(type);
            
            var expKey = PlayerPrefKeys.Experience;
            var newExpValue = _prefsManager.GetValue<int>(expKey) + plant.experienceReward;
            _prefsManager.SetValue(expKey, newExpValue);
            
            _cellView.Renderer.material = plant.grassMaterial;
            
            if(type == EPlantType.Carrot)
            {
                var carrotKey = PlayerPrefKeys.CarrotKey;
                var newValue = _prefsManager.GetValue<int>(carrotKey) + 1;
                _prefsManager.SetValue(carrotKey, newValue);
            }
            
            _plantView.DestroyView();
            _cellView.State = new EmptyCellState(_cellView).Initialize(_plantParameters);
        }
    }
}