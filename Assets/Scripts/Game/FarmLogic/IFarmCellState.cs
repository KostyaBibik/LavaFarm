using Enums;

namespace Game.FarmLogic
{
    public interface IFarmCellState
    {
        bool IsHandled { get; set; }
        void Handle(EPlantType type);
    }
}