using System;
using System.Collections.Generic;
using Db;
using Db.Impl;
using Enums;
using UnityEngine;

namespace Game.FarmLogic
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(CellPlantParameters),
        fileName = nameof(CellPlantParameters))]
    public class CellPlantParameters : ScriptableObject, IPlantBase
    {
        [SerializeField] private EPlantType defaultPlant;
        [SerializeField] private List<PlantVo> plants;

        public EPlantType DefaultPlant => defaultPlant;
        public List<PlantVo> AllPlants => plants;
        
        public PlantVo GetPlant(EPlantType plantType)
        {
            foreach (var plantVo in plants)
            {
                if (plantVo.plantType == plantType)
                    return plantVo;
            }
            
            throw new Exception($"[{nameof(CellPlantParameters)}] Cannot find PlantVo with type: {plantType}");
        }
    }
}