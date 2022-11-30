using System.Collections;
using Enums;
using UnityEngine;

namespace Game.Player
{
    public class PlayerHandlingSystem
    {
        private static readonly int PickUpHash = Animator.StringToHash("PickUp");
        private static readonly int ChopHash = Animator.StringToHash("Chop");
        
        private readonly PlayerView _playerView;
        private bool _isHandling;

        public PlayerHandlingSystem(PlayerView playerView)
        {
            _playerView = playerView;
        }

        public IEnumerator Handle(EPlayerState playerState, bool plantIsHandled)
        {
            _playerView.state = playerState;
            
            int operationHash;
            
            if(plantIsHandled)
            {
                switch (playerState)
                {
                    case EPlayerState.PickUp:
                        operationHash = PickUpHash;
                        break;
                    case EPlayerState.Chop:
                        operationHash = ChopHash;
                        break;
                    default:
                        yield break;
                }
            }
            else
            {
                operationHash = PickUpHash;
            }

            StartHandle(operationHash);

            yield return new WaitUntil(() => !_isHandling);
        }

        private void StartHandle(int operationHash)
        {
            _isHandling = true;
            _playerView.onEndHandle += FinishHandle;
            _playerView.onEndHandle += delegate
            {
                _playerView.Animator.SetBool(operationHash, false);
            };
            _playerView.Animator.SetBool(operationHash, true);
        }

        private void FinishHandle()
        {
            _playerView.onEndHandle -= FinishHandle;
            _playerView.state = EPlayerState.Idle;
            _isHandling = false;
        }
    }
}