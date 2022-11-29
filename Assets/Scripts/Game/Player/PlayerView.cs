using System;
using Enums;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        
        public NavMeshAgent NavMeshAgent => agent;

        public EPlayerState playerState;
        
        public PlayerView()
        {
            playerState = EPlayerState.Idle;
        }
        
        private void Start()
        {
            
        }
    }
}