using Game.FarmLogic.Impl;
using UnityEngine;
using Zenject;

namespace Game.FarmLogic
{
    public class FarmInitializeSystem : MonoBehaviour
    {
        private Vector2 _countCells;
        private FarmCellView _cellView;
        private float _widthCell;
        private float _depth;
        private Vector3 _sizeFarmBox;
        private Vector3 _minPointBox;

        [Inject]
        private void Construct(
            FarmGameParameters parameters,
            GameHolder gameHolder
            )
        {
            _countCells = new Vector2(parameters.CountCellsX, parameters.CountCellsY);
            _cellView = parameters.CellView;
            _sizeFarmBox = gameHolder.SizeFarmBox;
            _minPointBox = gameHolder.minPointBox;
        }

        private void Start()
        {
            FillFarm();
        }

        private void FillFarm()
        {
            var startPos = _minPointBox;
            
            CalculateParametersCells();
            
            startPos += new Vector3(.5f * _widthCell, 0f, .5f * _widthCell);
            var saveStartPos = startPos;
            
            Debug.Log($"_sizeFarmBox: {_sizeFarmBox}");

            var parentCells = new GameObject();
            parentCells.gameObject.name = "ParentCells";
            
            for (int i = 0; i < _countCells.x; i++)
            {
                for (int j = 0; j < _countCells.y; j++)
                {
                    CreateCell(startPos, parentCells.transform);
                    startPos += new Vector3(0f, 0f, _depth);
                }

                startPos = new Vector3(startPos.x + _widthCell, saveStartPos.y, saveStartPos.z);
            }
        }

        private void CalculateParametersCells()
        {
            _widthCell = _sizeFarmBox.x / _countCells.x;
            _depth = _sizeFarmBox.z / _countCells.y;
        }
        
        private IFarmCell CreateCell(Vector3 position, Transform parent)
        {
            var cell = Instantiate(_cellView, parent);

            var cellTransform = cell.transform;
            cellTransform.localScale = new Vector3(_widthCell, cellTransform.localScale.y, _depth);
            cellTransform.position = position;
            
            return cell;
        }
    }
}