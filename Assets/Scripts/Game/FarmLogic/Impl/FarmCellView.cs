using System;
using System.Collections;
using Db;
using Enums;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Game.FarmLogic.Impl
{
    public class FarmCellView : MonoBehaviour, IFarmCell
    {
        public IFarmCellState State { get; set; }
        public Transform posToSpawnPlant;
        public Transform posToSpawnUiView;
        
        [SerializeField] private MeshRenderer renderer;
        [DoNotSerialize] public CellGUIView CellGUIView { set; get; }
        
        public CellPlantParameters PlantParameters { get; private set; }
        public IPrefsManager PrefsManager { get; private set; }
        public EPlantType CurrentType { get; private set; }
        public MeshRenderer Renderer => renderer;
        
        private Action onStateChange;
        private NavMeshSurface _navMeshSurface;

        public void Handle(EPlantType type)
        {
            if(type == EPlantType.None)
            {
                State.Handle(CurrentType);
                return;
            }
            
            CurrentType = type;
            State.Handle(type);
            onStateChange?.Invoke();
        }
        
        [Inject]
        public void Construct(
            CellPlantParameters plantParameters,
            IPrefsManager prefsManager,
            NavMeshSurface navMeshSurface
        )
        {
            PlantParameters = plantParameters;
            PrefsManager = prefsManager;
            _navMeshSurface = navMeshSurface;
            onStateChange += RebuildNavMesh;
            State = new EmptyCellState(this).Initialize(plantParameters);
        }

        private void RebuildNavMesh()
        {
            StartCoroutine(nameof(RebuildWithDelay));
        }

        private IEnumerator RebuildWithDelay()
        {
            yield return null;
            
            _navMeshSurface.BuildNavMesh();
        }
    }
}