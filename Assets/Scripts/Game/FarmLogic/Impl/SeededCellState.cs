using UnityEngine;

namespace Game.FarmLogic.Impl
{
    public class SeededCellState : IFarmCellState
    {
        public void Tear(FarmCellView cellView)
        {
            cellView.State = new RipedCellState();
            Debug.Log("cellView.State RipedCellState");
        }

        public void Seed(FarmCellView cellView)
        {
            Debug.LogError("actually seed");
        }
    }
}