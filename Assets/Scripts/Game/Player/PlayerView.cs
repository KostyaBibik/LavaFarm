using System;
using Enums;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;
        
        public NavMeshAgent NavMeshAgent => agent;
        public Animator Animator => animator;

        public EPlayerState state;

        public event Action onEndHandle;
        
        public PlayerView()
        {
            state = EPlayerState.Idle;
        }

        public void EndHandle()
        {
            onEndHandle?.Invoke();
        }
    }
}