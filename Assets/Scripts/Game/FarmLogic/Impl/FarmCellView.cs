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
        
        private CellBlockParameters _blockParameters;
        public MeshRenderer Renderer => renderer;
        
        public FarmCellView()
        {
            State = new EmptyCellState(this);
        }
        
        public void Tear()
        {
            State.Tear(_blockParameters);
        }

        public void Seed()
        {
            State.Seed(_blockParameters);
        }

        [Inject]
        public void Construct(CellBlockParameters blockParameters)
        {
            _blockParameters = blockParameters;
        }
    }
}