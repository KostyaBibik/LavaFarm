using Game.FarmLogic.Impl;

namespace Game.FarmLogic
{
    public interface IFarmCellState
    {
        void Tear(CellBlockParameters blockParameters);
        void Seed(CellBlockParameters blockParameters);
    }
}