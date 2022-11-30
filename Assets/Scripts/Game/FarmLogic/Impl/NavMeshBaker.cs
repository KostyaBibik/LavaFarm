using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Game.FarmLogic.Impl
{
    public class NavMeshBaker : MonoBehaviour
    {
        public NavMeshSurface[] surfaces;
        public Transform[] objectsToRotate;

        private void Start()
        {
            for (int i = 0; i < objectsToRotate.Length; i++)
            {
                objectsToRotate[i].localRotation = Quaternion.Euler(
                    new Vector3(0, Random.Range(0, 360), 0)
                    );
            }

            foreach (var surface in surfaces)
            {
                surface.BuildNavMesh();
            }
        }
    }
}