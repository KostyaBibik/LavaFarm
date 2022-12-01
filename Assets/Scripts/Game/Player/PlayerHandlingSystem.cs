using System.Collections;
using Db;
using Enums;
using Game.Player.Equipment.Impl;
using UnityEngine;

namespace Game.Player
{
    public class PlayerHandlingSystem
    {
        private bool _isHandling;
        private GameObject _currentEquipment;
        
        private readonly PlayerView _playerView;
        private readonly ScytheEquipmentView _scytheEquipmentView;
        private readonly AxeEquipmentView _axeEquipmentView;
        
        public PlayerHandlingSystem(
            PlayerView playerView,
            ScytheEquipmentView scytheEquipmentView,
            AxeEquipmentView axeEquipmentView
            )
        {
            _playerView = playerView;
            _scytheEquipmentView = scytheEquipmentView;
            _axeEquipmentView = axeEquipmentView;
        }

        public IEnumerator Handle(EPlayerState playerState, bool plantIsHandled)
        {
            _playerView.state = playerState;
            
            int operationHash;
            var playerEquipment = EPlayerEquipment.None;
            
            if(plantIsHandled)
            {
                switch (playerState)
                {
                    case EPlayerState.PickUp:
                        operationHash = AnimatorHashKeys.PickUpHash;
                        break;
                    case EPlayerState.Chop:
                        playerEquipment = EPlayerEquipment.Axe;
                        operationHash = AnimatorHashKeys.ChopHash;
                        break;
                    case EPlayerState.Mow:
                        playerEquipment = EPlayerEquipment.Scythe;
                        operationHash = AnimatorHashKeys.MowHash;
                        break;
                    default:
                        yield break;
                }
            }
            else
            {
                operationHash = AnimatorHashKeys.PickUpHash;
            }

            StartHandle(operationHash, playerEquipment);

            yield return new WaitUntil(() => !_isHandling);
        }

        private void StartHandle(int operationHash, EPlayerEquipment playerEquipment)
        {
            _isHandling = true;
            _playerView.onEndHandle += FinishHandle;
            _playerView.onEndHandle += delegate
            {
                _playerView.Animator.SetBool(operationHash, false);
            };
            _playerView.Animator.SetBool(operationHash, true);

            switch (playerEquipment)
            {
                case EPlayerEquipment.Axe:
                    _currentEquipment = _axeEquipmentView.gameObject;
                    break;
                case EPlayerEquipment.Scythe:
                    _currentEquipment = _scytheEquipmentView.gameObject;
                    break;
            }

            SwitchEquipmentEnable();
        }

        private void FinishHandle()
        {
            _playerView.onEndHandle -= FinishHandle;
            _playerView.state = EPlayerState.Idle;
            _isHandling = false;
            SwitchEquipmentEnable();
            _currentEquipment = null;
        }

        private void SwitchEquipmentEnable()
        {
            if(!_currentEquipment)
                return;
            
            _currentEquipment.SetActive(!_currentEquipment.activeSelf);
        }
    }
}