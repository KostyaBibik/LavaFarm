using Enums;
using Game.FarmLogic;
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
        private CellPlantParameters _cellPlantParameters;
        
        public void Tick()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out RaycastHit hit, MaxDistance))
                {
                    if (hit.transform.TryGetComponent(out FarmCellView cellView))
                    {
                        if (_chosenCellView != cellView && _chosenCellView != null)
                        {
                            ClearSelectedView();
                            _chosenCellView = null;
                        }

                        var cellViewState = cellView.State;
                        if (cellViewState.IsHandled)
                        {
                            if(!cellViewState.IsRiped)
                                return;
                            
                            _playerMoveSystem.SetTargetCell(cellView);
                            return;
                        }
                        
                        cellView.CellGUIView.SwitchPlantPanelEnable(true);
                        cellView.Renderer.material = _cellPlantParameters.SelectedBlock;

                        cellView.CellGUIView.onChooseBtn += delegate(EPlantType type)
                        {
                            ChooseCellViewBtn(type);
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
        public void Construct(PlayerMoveSystem playerMoveSystem, CellPlantParameters cellPlantParameters)
        {
            _playerMoveSystem = playerMoveSystem;
            _cellPlantParameters = cellPlantParameters;
        }

        private void ChooseCellViewBtn(EPlantType type)
        {
            if (!_chosenCellView)
                return;
            
            ClearSelectedView();
            _chosenCellView = null;
        }

        private void ClearSelectedView()
        {
            _chosenCellView.Renderer.material = _cellPlantParameters.DefaultBlock;
            _chosenCellView.CellGUIView.onChooseBtn -= ChooseCellViewBtn;
            _chosenCellView.CellGUIView.SwitchGuiEnable(false);
        }
    }
}