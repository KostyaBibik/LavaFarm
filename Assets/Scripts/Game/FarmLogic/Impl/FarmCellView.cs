using Enums;
using UnityEngine;

namespace Game.FarmLogic.Impl
{
    public class FarmCellView : MonoBehaviour, IFarmCell
    {
        public IFarmCellState State { get; set; }

        public FarmCellView()
        {
            State = new EmptyCellState();
        }
        
        public void Tear()
        {
            State.Tear(this);
        }

        public void Seed()
        {
            State.Seed(this);
        }
    }
}