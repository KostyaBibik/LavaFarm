﻿using System;
using System.Collections;
using Enums;
using UniRx;
using UnityEngine;

namespace Game.Player
{
    public class PlayerMoveSystem : MonoBehaviour
    {
        private PlayerView _playerView;
        private Vector3 _startPos;
        public Transform destination;
        
        private void Awake()
        {
            _playerView = GetComponent<PlayerView>();
            _startPos = transform.position;
        }

        private void Start()
        {
            //_playerView.NavMeshAgent.SetDestination(destination.transform.position);
            SetDestination(destination.transform.position);
        }
        
        public void SetDestination(Vector3 targetPos)
        {
            if(_playerView.playerState != EPlayerState.Idle)
                return;
            
            StartCoroutine(nameof(Moving), targetPos);

            OnEndMoving();
        }

        private IEnumerator Moving(Vector3 targetPos)
        {
            _playerView.playerState = EPlayerState.Moving;
            _playerView.NavMeshAgent.SetDestination(targetPos);

            Debug.Log(_playerView.NavMeshAgent.destination);
            Debug.Log($"Still moving distance: {_playerView.NavMeshAgent.remainingDistance}");
            while (_playerView.NavMeshAgent.remainingDistance > .2f)
            {
                Debug.Log($"Still moving distance: {_playerView.NavMeshAgent.remainingDistance}");
                yield return null;
            }

            OnEndMoving();
        }

        private void OnEndMoving()
        {
            _playerView.playerState = EPlayerState.MovingToStartPos;
            _playerView.NavMeshAgent.SetDestination(_startPos);
        }
    }
}