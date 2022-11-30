using Enums;
using Game.FarmLogic.Impl;
using Game.Player;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Game.Interaction
{
    public class InputUpdateSystem : ITickable, IInitializable
    {
        private const float MaxDistance = 100f;
        
        private Camera _mainCamera;
        private FarmCellView _chosenCellView;
        
        private PlayerMoveSystem _playerMoveSystem; 
        
        public void Tick()
        {
            Debug.Log("Tick");
            
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out RaycastHit hit, MaxDistance))
                {
                    if (hit.transform.TryGetComponent(out FarmCellView cellView))
                    {
                        Debug.Log("HIt on cell");
                        if (_chosenCellView != cellView && _chosenCellView != null)
                        {
                            Debug.Log("Clear");
                            ClearSelectedView();
                            _chosenCellView = null;
                        }

                        if (cellView.State.IsHandled)
                        {
                            _playerMoveSystem.SetTargetCell(cellView);
                            return;
                        }
                        
                        cellView.CellGUIView.SwitchPlantPanelEnable(true);
                        cellView.CellGUIView.onChooseBtn += ChooseCellViewBtn;
                        cellView.CellGUIView.onChooseBtn += delegate(EPlantType type)
                        {
                            _playerMoveSystem.SetTargetCell(cellView, type);
                        };
                        
                        _chosenCellView = cellView;
                    }
                }
            }
        }

        public void Initialize()
        {
            _mainCamera = Camera.main;
        }

        [Inject]
        public void Construct(PlayerMoveSystem playerMoveSystem)
        {
            _playerMoveSystem = playerMoveSystem;
        }

        private void ChooseCellViewBtn(EPlantType type)
        {
            if(_chosenCellView)
            {
                ClearSelectedView();
                _chosenCellView = null;
            }
        }

        private void ClearSelectedView()
        {
            _chosenCellView.CellGUIView.onChooseBtn -= ChooseCellViewBtn;
            _chosenCellView.CellGUIView.SwitchGuiEnable(false);
        }
    }
}