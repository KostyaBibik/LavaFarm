using UnityEngine;
using Zenject;

namespace Game.FarmLogic.Impl
{
    public class EmptyCellState : IFarmCellState
    {
        private readonly FarmCellView _cellView;

        public EmptyCellState(FarmCellView cellView)
        {
            _cellView = cellView;
        }
        
        public void Tear(CellBlockParameters blockParameters)
        {
            throw new System.NotImplementedException();
        }

        public void Seed(CellBlockParameters blockParameters)
        {
            Debug.Log($"Seed {blockParameters.SeededBlockPrefab.name}");
            _cellView.State = new SeededCellState(blockParameters.TimeToRipening, _cellView);
            _cellView.Renderer.material = blockParameters.GrassMaterial;
        }
    }
}