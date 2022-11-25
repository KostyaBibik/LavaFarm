using System.Collections.Generic;
using Game.FarmLogic.Impl;
using UnityEngine;
using Zenject;

namespace Game.FarmLogic
{
    public class FarmInitializeSystem : MonoBehaviour
    {
        private Vector2 _countCells;
        private FarmCellView _cellView;
        private Vector3 _sizeCell;
        private Vector3 _sizeFarmBox;
        private Vector3 _minPointBox;

        private readonly List<FarmCellView> _cells = new List<FarmCellView>();
        
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
            
            startPos += new Vector3(.5f * _sizeCell.x, .5f * _sizeCell.y, .5f * _sizeCell.z);
            var savedStartPos = startPos;

            var parentCells = new GameObject();
            parentCells.gameObject.name = "ParentCells";
            
            for (int i = 0; i < _countCells.x; i++)
            {
                for (int j = 0; j < _countCells.y; j++)
                {
                    _cells.Add(CreateCell(startPos, parentCells.transform));
                    startPos += new Vector3(0f, 0f, _sizeCell.z);
                }

                startPos = new Vector3(startPos.x + _sizeCell.x, savedStartPos.y, savedStartPos.z);
            }
            
            Debug.Log($"_cells: {_cells.Count}");
        }

        private void CalculateParametersCells()
        {  
            var widthCell = _sizeFarmBox.x / _countCells.x;
            var depthCell = _sizeFarmBox.z / _countCells.y;
            var heightCell = _sizeFarmBox.y;
            
            _sizeCell = new Vector3(widthCell, heightCell, depthCell);
        }
        
        private FarmCellView CreateCell(Vector3 position, Transform parent)
        {
            var cell = Instantiate(_cellView, parent);

            var cellTransform = cell.transform;
            cellTransform.localScale = new Vector3(_sizeCell.x, cellTransform.localScale.y, _sizeCell.z);
            cellTransform.position = position;
            
            return cell;
        }
    }
}