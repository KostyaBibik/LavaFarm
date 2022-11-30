using System.Collections;
using System.Collections.Generic;
using Enums;
using Game.FarmLogic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerMoveSystem
    {
        [Inject] private PlayerView _playerView;
        private Vector3 _startPos;
        private Queue<Vector3> _targetGoals = new Queue<Vector3>();

        public PlayerMoveSystem(/*PlayerView playerView,*/ GameHolder gameHolder)
        {
            //_playerView = playerView;
            _startPos = gameHolder.SpawnPointPlayer;
        }

        /*[Inject]
        public void Construct(PlayerView playerView)
        {
            _playerView = playerView;
        }*/

        public void SetDestination(Vector3 targetPos)
        {
            if(_playerView.playerState != EPlayerState.Idle && _playerView.playerState != EPlayerState.MovingToStartPos)
            {
                _targetGoals.Enqueue(targetPos);
                return;
            }
            
            Observable.FromCoroutine(() => Moving(targetPos))
                .DoOnCompleted(DoS)
                .Subscribe();
            //StartCoroutine(nameof(Moving), targetPos);
        }

        private IEnumerator Moving(Vector3 targetPos)
        {
            _playerView.playerState = EPlayerState.Moving;
            _playerView.NavMeshAgent.SetDestination(targetPos);
            
            do
            {
                 yield return null;
            } while (_playerView.NavMeshAgent.remainingDistance > .2f);

            if (_targetGoals.Count > 0)
            {
                Observable.FromCoroutine(() => Moving(_targetGoals.Dequeue()))
                    .DoOnCompleted(DoS)
                    .Subscribe();
                //StartCoroutine(Moving(_targetGoals.Dequeue()));
            }
            else
            {
                OnEndMoving();
            }
        }

        private void DoS()
        {
            
        }
        
        private void OnEndMoving()
        {
            _playerView.playerState = EPlayerState.MovingToStartPos;
            _playerView.NavMeshAgent.SetDestination(_startPos);
        }
    }
}