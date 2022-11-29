using UnityEngine;

namespace Game.FarmLogic
{
    public class GameHolder : MonoBehaviour
    {
        [SerializeField] private Transform spawnPointPlayer;
        [SerializeField] private Collider farmBoxBounds;
        [SerializeField] private Transform spawnPointGround;
        
        public Vector3 SpawnPointPlayer => spawnPointPlayer.position;
        public Vector3 SizeFarmBox => farmBoxBounds.bounds.size;
        public Vector3 minPointBox => farmBoxBounds.bounds.min;
        public Vector3 SpawnPointGround => spawnPointGround.position;
    }
}