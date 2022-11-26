using UnityEngine;

namespace Game.FarmLogic.Impl
{
    public class RipedCellState : IFarmCellState
    {
        private readonly FarmCellView _cellView;
        
        public RipedCellState(FarmCellView cellView)
        {
            _cellView = cellView;
            Debug.Log("Has riped");
        }
        
        public void Tear(CellBlockParameters blockParameters)
        {
            throw new System.NotImplementedException();
        }

        public void Seed(CellBlockParameters blockParameters)
        {
            throw new System.NotImplementedException();
        }
    }
}