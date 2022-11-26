using Game.FarmLogic.Impl;
using UnityEngine;

namespace Game.FarmLogic
{
    public interface IFarmCellFactory
    {
        FarmCellView CreateBlock();

        CellGUIView CreateUiView();
    }
}