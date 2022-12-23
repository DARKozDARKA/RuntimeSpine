using System;
using CodeBase.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
    
        [SerializeField]
        private float _slopeSpeed;
    
        [SerializeField]
        private LayerMask _ground;

        [SerializeField]
        private float _slopeAngle;
    
        [SerializeField]
        private float _slopeBoost;

        private float _lastSlopeAngle;
        private Vector2 _slopeNormalPerpendicular;
        private bool _onSlope;
        private Vector2 _currentSpeed;
        private Vector2 _oldSpeed;
        private Vector2 _slopeVelocity;
        private float _slopePower;
        private float _currentAxis;

        private MovementDirections _currentMovementDirection;
        private MovementDirections _oldMovementDirection;
    
        private PlayerMovementStates _currentMovementState;
        private PlayerMovementStates _oldMovementState;

        public Action<MovementDirections> OnChangedDirection;
        public Action<PlayerMovementStates> OnChangedMovementState;
        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService) => 
            _inputService = inputService;

        private void Update()
        {
            GetHorizontalInput();
            SetGround();
            SetVelocity();
            MoveObject();
            InvokeActions();
        }

        private void SetVelocity()
        {
            if (_onSlope)
            {
                CalculateCurrentSpeedForSlope();
                AddSlopeVelocity();
            }
            else
                CalculateCurrentSpeepForPlayerControl();
        }

        private void InvokeActions()
        {
            SetNewMovementDirection();
            CheckIfMovementDirectionChanged();

            CheckIfIdle();
            CheckIfRunning();
            CheckIfHovering();
            InvokeMovementState();
        }

        private void SetGround()
        {
            RaycastHit2D hit = GetRaycastHitToGround();

            if (!hit) 
                return;

            if (CheckIfSlope(hit))
                SetSlope();
            else
                SetNormal();
        }

        private void MoveObject() => 
            transform.Translate(_currentSpeed);

        private void AddSlopeVelocity() => 
            _currentSpeed = _slopeVelocity;

        private void CalculateCurrentSpeepForPlayerControl() => 
            _currentSpeed = _slopeNormalPerpendicular * (Time.deltaTime * _speed * _currentAxis * -1);

        private void CalculateCurrentSpeedForSlope()
        {
            _slopePower += Time.deltaTime;
            Vector2 slopeSpeed = _slopeNormalPerpendicular * (_slopeSpeed * _slopePower);
            Vector2 inertia = CalculateInertia();
            _slopeVelocity = slopeSpeed + inertia;

            Vector2 CalculateInertia()
            {
                Vector2 velocity = _slopeNormalPerpendicular.normalized * _slopeBoost;
                int direction = _oldSpeed.x * _slopeNormalPerpendicular.x > 0 ? 1 : -1;
                float power = _slopePower < 1 ? 1 - _slopePower : 0;

                return velocity * (direction * power);
            }
        }

        private RaycastHit2D GetRaycastHitToGround() =>
            Physics2D.Raycast(transform.position, Vector2.down, 9999f, _ground);

        private bool CheckIfSlope(RaycastHit2D hit)
        {
            _slopeNormalPerpendicular = Vector2.Perpendicular(hit.normal).normalized;
            float slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            float dotProduct = Vector2.Dot(hit.normal, Vector2.right);

            if (dotProduct > 0)
                _slopeNormalPerpendicular *= -1;

            return GroundAngleIsSlope();

            bool GroundAngleIsSlope() => 
                slopeDownAngle >= _slopeAngle;
        }
    
        private void SetSlope()
        {
            if (OnSlope())
                return;
        
            _oldSpeed = _currentSpeed;
            _onSlope = true;
        }

        private void SetNormal()
        {
            if (OnSlope() == false)
                return;
        
            _onSlope = false;
            _slopeVelocity = Vector2.zero;
            _slopePower = 0;
        }
    
        private bool OnSlope() => 
            _onSlope;
    

        private void GetHorizontalInput() => 
            _currentAxis = _inputService.GetHorizontal();

        private void SetNewMovementDirection() =>
            _currentMovementDirection = _currentSpeed.x switch
            {
                > 0 => MovementDirections.Right,
                < 0 => MovementDirections.Left,
                _ => _currentMovementDirection
            };

        private void InvokeMovementState()
        {
            if (_oldMovementState == _currentMovementState)
                return;

            _oldMovementState = _currentMovementState;
            OnChangedMovementState?.Invoke(_currentMovementState);
        }

        private void CheckIfIdle()
        {
            if (!NoSpeed()) 
                return;
        
            _currentMovementState = PlayerMovementStates.Idle;
        }
    
        private void CheckIfRunning()
        {
            if (NoSpeed() || _onSlope) 
                return;
        
            _currentMovementState = PlayerMovementStates.Running;
        }
    
        private void CheckIfHovering()
        {
            if (NoSpeed() || !_onSlope) 
                return;
        
            _currentMovementState = PlayerMovementStates.Hovering;
        }

        private bool NoSpeed() => 
            _currentSpeed.magnitude == 0;

        private void CheckIfMovementDirectionChanged()
        {
            if (_currentMovementDirection == _oldMovementDirection)
                return;
        
            _oldMovementDirection = _currentMovementDirection;
            OnChangedDirection?.Invoke(_currentMovementDirection);

        }
    }
}