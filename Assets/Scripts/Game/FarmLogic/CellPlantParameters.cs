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
        [SerializeField] private GameObject emptyBlockPrefab;
        [SerializeField] private GameObject cellGuiPrefab;

        [Header("Materials")]
        [SerializeField] private Material selectedBlock;
        [SerializeField] private Material defaultBlock;
        [SerializeField] private Material plantedBlock;
        [SerializeField] private Material ripedBlock;
        
        
        
        [Space, SerializeField] private List<PlantVo> plants;

        public Material SelectedBlock => selectedBlock;
        public Material DefaultBlock => defaultBlock;
        public Material PlantedBlock => plantedBlock;
        public Material RipedBlock => ripedBlock;
        public GameObject EmptyBlockPrefab => emptyBlockPrefab;
        public GameObject CellGuiPrefab => cellGuiPrefab;
        
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