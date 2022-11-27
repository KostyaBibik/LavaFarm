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
        public MeshRenderer Renderer => renderer;

        public void Handle(EPlantType type = EPlantType.None)
        {
            State.Handle(type);
        }

        [Inject]
        public void Construct(CellPlantParameters plantParameters)
        {
            PlantParameters = plantParameters;
            State = new EmptyCellState(this).Initialize(plantParameters);
        }
    }
}