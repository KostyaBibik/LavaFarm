using System;
using Enums;
using Game.FarmLogic.Impl;
using UnityEngine;

namespace Db.Impl
{
    [Serializable]
    public struct PlantVo
    {
        public EPlantType plantType;
        
        public GameObject emptyBlockPrefab;
        public GameObject guiCellPrefab;
        public PlantView plantView;
        
        public Material grassMaterial;
        public Material ripeMaterial;
        
        public Vector3 endGrowScale;
        public float onEndGrowHeightPos;
        
        public float timeToRipening;
        public int experienceReward;
    }
}