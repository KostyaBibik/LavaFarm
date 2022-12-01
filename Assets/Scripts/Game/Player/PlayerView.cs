using System;
using Enums;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Player
{
    public class PlayerView : MonoBehaviour
    {
        public EPlayerState state;
        
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform scytheHolder;
        [SerializeField] private Transform axeHolder;
        
        public NavMeshAgent NavMeshAgent => agent;
        public Animator Animator => animator;
        public Transform ScytheHolder => scytheHolder;
        public Transform AxeHolder => axeHolder;
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