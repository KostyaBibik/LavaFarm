using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Db;
using Enums;
using Game.FarmLogic;
using Game.FarmLogic.Impl;
using UniRx;
using UnityEngine;

namespace Game.Player
{
    public class PlayerMoveSystem
    {
        private readonly Queue<Tuple<FarmCellView, EPlantType>> _targetCells = new Queue<Tuple<FarmCellView, EPlantType>>();
        private readonly Vector3 _startPos;
        private readonly PlayerView _playerView;
        private readonly PlayerHandlingSystem _handlingSystem;
        private const float _minDistanceToTarget = 1f;

        public PlayerMoveSystem(PlayerView playerView, GameHolder gameHolder, PlayerHandlingSystem handlingSystem)
        {
            _playerView = playerView;
            _startPos = gameHolder.SpawnPointPlayer;
            _handlingSystem = handlingSystem;
        }

        public void SetTargetCell(FarmCellView targetCell, EPlantType type = EPlantType.None)
        {
            if (_targetCells.Any(addedCell
                => addedCell.Item1 == targetCell))
                return;

            if (type == EPlantType.None)
                type = targetCell.CurrentType;
            
            var cellTuple = new Tuple<FarmCellView, EPlantType>(targetCell, type);
            _targetCells.Enqueue(cellTuple);
            
            if(_playerView.state != EPlayerState.Idle &&
               _playerView.state != EPlayerState.MovingToStartPos)
            {
                return;
            }
            
            Observable.FromCoroutine(() => Moving(targetCell.transform.position))
                .Subscribe();
        }

        private IEnumerator Moving(Vector3 targetPos)
        {
            _playerView.NavMeshAgent.isStopped = false;
            _playerView.state = EPlayerState.Moving;
            _playerView.NavMeshAgent.SetDestination(targetPos);
            _playerView.Animator.SetTrigger(AnimatorHashKeys.MoveHash);
            
            yield return null;
            
            yield return new WaitUntil(()
                => _playerView.NavMeshAgent.remainingDistance < _minDistanceToTarget);
            _playerView.NavMeshAgent.isStopped = true;

            var playerHandleState = EPlayerState.Idle;
            switch (_targetCells.Peek().Item2)
            {
                case EPlantType.Carrot:
                    playerHandleState = EPlayerState.PickUp;
                    break;
                case EPlantType.Tree:
                    playerHandleState = EPlayerState.Chop;
                    break;
                case EPlantType.Grass:
                    playerHandleState = EPlayerState.Mow;
                    break;
            }

            yield return _handlingSystem.Handle(
                playerHandleState,
                _targetCells.Peek().Item1.State.IsHandled
                );
            
            HandleSelectedCell();
            
            if (_targetCells.Count > 0)
            {
                Observable.FromCoroutine(() =>
                        Moving(_targetCells.Peek().Item1.transform.position)).Subscribe();
            }
            else
            {
                OnEndMoving();
            }
        }

        private void HandleSelectedCell()
        {
            var handledCell = _targetCells.Dequeue();
            handledCell.Item1.Handle(handledCell.Item2);
        }
        
        private void OnEndMoving()
        {
            _playerView.state = EPlayerState.MovingToStartPos;
            _playerView.NavMeshAgent.isStopped = false;
            _playerView.NavMeshAgent.SetDestination(_startPos);
            _playerView.Animator.SetTrigger(AnimatorHashKeys.MoveHash);
        }
    }
}