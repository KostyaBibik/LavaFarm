using UnityEngine;

namespace Game.FarmLogic
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(CellBlockParameters),
        fileName = nameof(CellBlockParameters))]
    public class CellBlockParameters : ScriptableObject
    {
        [SerializeField] private GameObject emptyBlockPrefab;
        [SerializeField] private GameObject seededBlockPrefab;
        [SerializeField] private GameObject ripedBlockPrefab;
        [SerializeField] private GameObject guiCellPrefab;
        
        [SerializeField] private Material grassMaterial;

        [SerializeField] private float timeToRipening;
        
        
        public GameObject EmptyBlockPrefab => emptyBlockPrefab;
        public GameObject SeededBlockPrefab => seededBlockPrefab;
        public GameObject RipedBlockPrefab => ripedBlockPrefab;
        public GameObject GuiCellPrefab => guiCellPrefab;
        public Material GrassMaterial => grassMaterial;
        public float TimeToRipening => timeToRipening;
    }
}