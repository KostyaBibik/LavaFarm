using System;
using Enums;
using UnityEngine;

namespace Db.Impl
{
    [Serializable]
    public struct PlantVo
    {
        public EPlantType plantType;
        
        public GameObject emptyBlockPrefab;
        public GameObject seededBlockPrefab;
        public GameObject ripedBlockPrefab;
        public GameObject guiCellPrefab;
        
        public Material grassMaterial;
        public Material ripeMaterial;
        
        public float timeToRipening;
    }
}