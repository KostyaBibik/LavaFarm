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
        
        public PlantView plantView;
        
        public Vector3 endGrowScale;
        public float onEndGrowHeightPos;
        
        public float timeToRipening;
        public int experienceReward;
    }
}