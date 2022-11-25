using UnityEngine;

namespace Game.FarmLogic.Impl
{
    public class EmptyCellState : IFarmCellState
    {
        public void Tear(FarmCellView cellView)
        {
            throw new System.NotImplementedException();
        }

        public void Seed(FarmCellView cellView)
        {
            Debug.Log("Seed");
            cellView.State = new SeededCellState();
        }
    }
}