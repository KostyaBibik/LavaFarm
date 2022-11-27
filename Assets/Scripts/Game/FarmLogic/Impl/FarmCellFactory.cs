using Enums;
using UnityEngine;
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
        
        public FarmCellView CreateBlock(EPlantType plantType)
        {
            return _diContainer
                .InstantiatePrefab(_cellPlantParameters.GetPlant(plantType).emptyBlockPrefab).GetComponent<FarmCellView>();
        }

        public CellGUIView CreateUiView(EPlantType plantType)
        {
            return _diContainer
                .InstantiatePrefab(_cellPlantParameters.GetPlant(plantType).guiCellPrefab).GetComponent<CellGUIView>().InitializeBtns();
        }
    }
}