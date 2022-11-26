using UnityEngine;

namespace Game.FarmLogic.Impl
{
    public class RipedCellState : IFarmCellState
    {
        public RipedCellState()
        {
            Debug.Log("Has riped");
        }
        
        public void Tear(FarmCellView cellView, CellBlockParameters blockParameters)
        {
            throw new System.NotImplementedException();
        }

        public void Seed(FarmCellView cellView, CellBlockParameters blockParameters)
        {
            throw new System.NotImplementedException();
        }
    }
}