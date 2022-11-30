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
        public Transform posToSpawnPlant;
        
        [SerializeField] private MeshRenderer renderer;
        [DoNotSerialize] public CellGUIView CellGUIView { set; get; }
        
        public CellPlantParameters PlantParameters { get; private set; }
        public IPrefsManager PrefsManager { get; private set; }
        public MeshRenderer Renderer => renderer;

        public EPlantType CurrentType { get; private set; }
        
        public void Handle(EPlantType type)
        {
            if(type == EPlantType.None)
            {
                State.Handle(CurrentType);
                return;
            }
            
            CurrentType = type;
            State.Handle(type);
        }

        public void Handle()
        {
            State.Handle(CurrentType);
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