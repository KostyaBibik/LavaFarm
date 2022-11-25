using Game.FarmLogic.Impl;

namespace Game.FarmLogic
{
    public interface IFarmCellState
    {
        void Tear(FarmCellView cellView);
        void Seed(FarmCellView cellView);
    }
}