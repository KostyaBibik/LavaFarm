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
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (!_playerMoveSystem)
                _playerMoveSystem = Object.FindObjectOfType<PlayerMoveSystem>();
            
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

                        if (cellView.State.IsHandled)
                        {
                            cellView.Handle();
                            return;
                        }
                        
                        cellView.CellGUIView.SwitchPlantPanelEnable(true);
                        cellView.CellGUIView.onChooseBtn += ChooseCellViewBtn;
                        cellView.CellGUIView.onChooseBtn += delegate(EPlantType type)
                        {
                            _playerMoveSystem.SetDestination(cellView.transform.position);
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

        private void ChooseCellViewBtn(EPlantType type)
        {
            ClearSelectedView();
            _chosenCellView.Handle(type);
            _chosenCellView = null;
        }

        private void ClearSelectedView()
        {
            _chosenCellView.CellGUIView.onChooseBtn -= ChooseCellViewBtn;
            _chosenCellView.CellGUIView.SwitchGuiEnable(false);
        }
    }
}