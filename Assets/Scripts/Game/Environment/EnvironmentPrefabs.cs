using Game.Player;
using UnityEngine;

namespace Game.Environment
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(EnvironmentPrefabs),
        fileName = nameof(EnvironmentPrefabs))]
    public class EnvironmentPrefabs : ScriptableObject
    {
        [SerializeField] private PlayerView playerView;
        
        [SerializeField] private GameObject ground;
        
        public GameObject Ground => ground;
        public PlayerView PlayerView => playerView;
    }
}