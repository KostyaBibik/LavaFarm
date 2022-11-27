using Enums;
using Game.FarmLogic.Impl;
using UnityEngine;

namespace Game.FarmLogic
{
    public interface IFarmCellFactory
    {
        FarmCellView CreateBlock(EPlantType plantType);

        CellGUIView CreateUiView(EPlantType plantType);
    }
}