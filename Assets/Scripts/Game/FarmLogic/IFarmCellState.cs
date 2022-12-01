using Enums;

namespace Game.FarmLogic
{
    public interface IFarmCellState
    {
        bool IsHandled { get; set; }
        bool IsRiped { get; set; }
        void Handle(EPlantType type);
    }
}