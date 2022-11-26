using UnityEngine;
using Zenject;

namespace Game.FarmLogic.Impl
{
    public class FarmCellFactory : IFarmCellFactory
    {
        private readonly DiContainer _diContainer;
        private readonly CellBlockParameters _cellBlockParameters;
        
        public FarmCellFactory(
            DiContainer diContainer,
            CellBlockParameters blockParameters
        )
        {
            _diContainer = diContainer;
            _cellBlockParameters = blockParameters;
        }
        
        public FarmCellView CreateBlock()
        {
            return _diContainer
                .InstantiatePrefab(_cellBlockParameters.EmptyBlockPrefab).GetComponent<FarmCellView>();
        }

        public CellGUIView CreateUiView()
        {
            return _diContainer
                .InstantiatePrefab(_cellBlockParameters.GuiCellPrefab).GetComponent<CellGUIView>();
        }
    }
}