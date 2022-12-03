using Zenject;

namespace Game.FarmLogic.Impl
{
    public class FarmCellFactory : IFarmCellFactory
    {
        private readonly DiContainer _diContainer;
        private readonly CellPlantParameters _cellPlantParameters;
        
        public FarmCellFactory(
            DiContainer diContainer,
            CellPlantParameters plantParameters
        )
        {
            _diContainer = diContainer;
            _cellPlantParameters = plantParameters;
        }
        
        public FarmCellView CreateBlock()
        {
            return _diContainer
                .InstantiatePrefab(_cellPlantParameters.EmptyBlockPrefab).GetComponent<FarmCellView>();
        }

        public CellGUIView CreateUiView()
        {
            return _diContainer
                .InstantiatePrefab(_cellPlantParameters.CellGuiPrefab).GetComponent<CellGUIView>().InitializeBtns();
        }
    }
}