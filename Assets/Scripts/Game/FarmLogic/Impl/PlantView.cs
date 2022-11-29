using System;
using UnityEngine;

namespace Game.FarmLogic.Impl
{
    public class PlantView : MonoBehaviour
    {
        public void DestroyView()
        {
            Destroy(gameObject);
        }
    }
}