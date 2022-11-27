using Enums;
using Game.FarmLogic.Impl;
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

                        if (cellView.State.IsHandled)
                        {
                            Debug.Log("IsHandled");
                            cellView.Handle();
                            return;
                        }
                        
                        cellView.CellGUIView.SwitchPlantPanelEnable(true);
                        cellView.CellGUIView.onChooseBtn += ChooseCellViewBtn;
                        
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
            
            Debug.Log($"Choose {type.ToString()}");
        }

        private void ClearSelectedView()
        {
            _chosenCellView.CellGUIView.onChooseBtn -= ChooseCellViewBtn;
            _chosenCellView.CellGUIView.SwitchGuiEnable(false);
        }
    }
}