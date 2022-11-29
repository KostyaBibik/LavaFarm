using Db;
using Enums;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Game.FarmLogic.Impl
{
    public class FarmCellView : MonoBehaviour, IFarmCell
    {
        public IFarmCellState State { get; set; }

        [SerializeField] private MeshRenderer renderer;
        [DoNotSerialize] public CellGUIView CellGUIView { set; get; }
        
        public CellPlantParameters PlantParameters { get; private set; }
        public IPrefsManager PrefsManager { get; private set; }
        public MeshRenderer Renderer => renderer;
        public Transform posToSpawnPlant;
        
        private EPlantType _currentType;
        
        public void Handle(EPlantType type)
        {
            _currentType = type;
            State.Handle(type);
        }

        public void Handle()
        {
            State.Handle(_currentType);
        }
        
        [Inject]
        public void Construct(CellPlantParameters plantParameters, IPrefsManager prefsManager)
        {
            PlantParameters = plantParameters;
            PrefsManager = prefsManager;
            State = new EmptyCellState(this).Initialize(plantParameters);
        }
    }
}