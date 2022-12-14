using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Game.FarmLogic
{
    public class FarmInitializeSystem : MonoBehaviour
    {
        private Vector2 _countCells;
        private Vector3 _sizeCell;
        private Vector3 _sizeFarmBox;
        private Vector3 _minPointBox;

        private IFarmCellFactory _cellFactory;
        private NavMeshSurface _navMeshSurface;
        
        [Inject]
        private void Construct(
            FarmGameParameters parameters,
            GameHolder gameHolder,
            IFarmCellFactory cellFactory,
            NavMeshSurface meshSurface
            )
        {
            _countCells = new Vector2(parameters.CountCellsX, parameters.CountCellsY);
            _sizeFarmBox = gameHolder.SizeFarmBox;
            _minPointBox = gameHolder.minPointBox;
            _cellFactory = cellFactory;
            _navMeshSurface = meshSurface;
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
                    CreateCell(startPos, parentCells.transform);
                    startPos += new Vector3(0f, 0f, _sizeCell.z);
                }

                startPos = new Vector3(startPos.x + _sizeCell.x, savedStartPos.y, savedStartPos.z);
            }

            _navMeshSurface.BuildNavMesh();
        }

        private void CalculateParametersCells()
        {  
            var widthCell = _sizeFarmBox.x / _countCells.x;
            var depthCell = _sizeFarmBox.z / _countCells.y;
            var heightCell = _sizeFarmBox.y;
            
            _sizeCell = new Vector3(widthCell, heightCell, depthCell);
        }
        
        private void CreateCell(Vector3 position, Transform parent)
        {
            var cellView = _cellFactory.CreateBlock();

            var cellTransform = cellView.transform;
            cellTransform.localScale = new Vector3(_sizeCell.x, cellTransform.localScale.y, _sizeCell.z);
            cellTransform.position = position;
            cellTransform.SetParent(parent);
            
            var cellUiView = _cellFactory.CreateUiView();

            cellView.CellGUIView = cellUiView;
            cellUiView.SwitchGuiEnable(false);
            
            var guiViewTransform = cellUiView.transform;
            guiViewTransform.position = cellView.posToSpawnUiView.position;
            guiViewTransform.SetParent(cellTransform);
        }
    }
}