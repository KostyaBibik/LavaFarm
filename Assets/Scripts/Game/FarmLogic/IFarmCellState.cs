using Game.FarmLogic.Impl;

namespace Game.FarmLogic
{
    public interface IFarmCellState
    {
        void Tear(FarmCellView cellView, CellBlockParameters blockParameters);
        void Seed(FarmCellView cellView, CellBlockParameters blockParameters);
    }
}