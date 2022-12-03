using Game.FarmLogic.Impl;

namespace Game.FarmLogic
{
    public interface IFarmCellFactory
    {
        FarmCellView CreateBlock();

        CellGUIView CreateUiView();
    }
}