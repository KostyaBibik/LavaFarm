using UnityEngine;
using Zenject;

namespace Game.FarmLogic.Impl
{
    public class EmptyCellState : IFarmCellState
    {
        public void Tear(FarmCellView cellView, CellBlockParameters blockParameters)
        {
            throw new System.NotImplementedException();
        }

        public void Seed(FarmCellView cellView, CellBlockParameters blockParameters)
        {
            Debug.Log($"Seed {blockParameters.SeededBlockPrefab.name}");
            cellView.State = new SeededCellState(blockParameters.TimeToRipening, cellView);
            cellView.Renderer.material = blockParameters.GrassMaterial;
        }
    }
}