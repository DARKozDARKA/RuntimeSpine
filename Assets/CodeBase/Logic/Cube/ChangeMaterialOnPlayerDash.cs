using CodeBase.Logic.Player;
using UnityEngine;

namespace CodeBase.Logic.Cube
{
    public class ChangeMaterialOnPlayerDash : MonoBehaviour
    {
        private PlayerMovement _playerMovement;

        [SerializeField]
        private MaterialChanger _materialChanger;

        private bool _isTriggered;

        public void SetPlayerMovement(PlayerMovement movement)
        { 
            _playerMovement = movement;
            _playerMovement.OnChangedMovementState += CheckMovementStateChange;
        }
    
        private void OnDisable()
        {
            if (_playerMovement == null)
                return;
        
            _playerMovement.OnChangedMovementState -= CheckMovementStateChange;
        }

        private void CheckMovementStateChange(PlayerMovementStates state)
        {
            switch (state)
            {
                case PlayerMovementStates.Hovering:
                    _materialChanger.SetSecondaryMateirial();
                    _isTriggered = true;
                    break;
                default:
                    if (_isTriggered == false)
                        break;
                
                    _materialChanger.SetMainMaterial();
                    _isTriggered = false;
                    break;
            }
        }
    }
}
