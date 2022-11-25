using Game.FarmLogic.Impl;
using UnityEngine;

namespace Game.FarmLogic
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(FarmGameParameters),
        fileName = nameof(FarmGameParameters))]
    public class FarmGameParameters : ScriptableObject
    {
        [SerializeField] private int countCellsX;
        [SerializeField] private int countCellsY;
        [SerializeField] private FarmCellView cellView;
        
        public int CountCellsX => countCellsX;
        public int CountCellsY => countCellsY;
        public FarmCellView CellView => cellView;
    }
}