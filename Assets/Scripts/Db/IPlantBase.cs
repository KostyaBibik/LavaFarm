using System.Collections.Generic;
using Db.Impl;
using Enums;

namespace Db
{
    public interface IPlantBase
    {
        List<PlantVo> AllPlants { get; }
        PlantVo GetPlant(EPlantType plantType);
    }
}