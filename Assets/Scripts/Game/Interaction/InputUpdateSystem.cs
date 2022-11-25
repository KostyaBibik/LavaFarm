using Game.FarmLogic.Impl;
using UnityEngine;
using Zenject;

namespace Game.Interaction
{
    public class InputUpdateSystem : ITickable, IInitializable
    {
        private Camera _mainCamera;
        private const float MaxDistance = 100f;
        
        public void Tick()
        {
            Debug.Log($"Tick {_mainCamera.name}");

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, MaxDistance))
                {
                    Debug.Log($"Hit {hit.transform.name}");
                    if (hit.transform.TryGetComponent(out FarmCellView cellView))
                    {
                        cellView.Seed();
                    }
                }
            }
        }

        public void Initialize()
        {
            _mainCamera = Camera.main;
        }
    }
}