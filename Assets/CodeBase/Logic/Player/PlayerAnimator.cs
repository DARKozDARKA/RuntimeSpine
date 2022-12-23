using System;
using Spine;
using Spine.Unity;
using UnityEngine;
using AnimationState = Spine.AnimationState;

namespace CodeBase.Logic.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField]
        private SkeletonAnimation _skeletonAnimation;
    
        [SerializeField]
        private AnimationReferenceAsset _runAnimation;
    
        [SerializeField]
        private AnimationReferenceAsset _idleAnimation;
    
        [SerializeField]
        private AnimationReferenceAsset _hoverAnimation;

        [SerializeField]
        private PlayerMovement _playerMovement;

        private AnimationState _animationState;
        private Skeleton _animationSkeleton;
        private PlayerMovementStates _playerMovementState;

        private void OnEnable()
        {
            _playerMovement.OnChangedDirection += ChangeDirection;
            _playerMovement.OnChangedMovementState += ChangeMovementState;
        }
    
        private void OnDisable()
        {
            _playerMovement.OnChangedDirection -= ChangeDirection;
            _playerMovement.OnChangedMovementState -= ChangeMovementState;
        }

        private void ChangeMovementState(PlayerMovementStates state)
        {
            switch (state)
            {
                case PlayerMovementStates.Idle:
                    PlayIdleAnimation();
                    break;
                case PlayerMovementStates.Running:
                    PlayRunAnimation();
                    break;
                case PlayerMovementStates.Hovering:
                    PlayHoveringAnimation();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void Start()
        {
            _animationState = _skeletonAnimation.AnimationState;
            _animationSkeleton = _skeletonAnimation.skeleton;

            PlayIdleAnimation();
        }

        private void ChangeDirection(MovementDirections directions)
        {
            switch (directions)
            {
                case MovementDirections.Left:
                    SetLeftPointing();
                    break;
                case MovementDirections.Right:
                    SetRightPointing();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(directions), directions, null);
            }
        }

        private void PlayRunAnimation()
        {
            _animationState.SetAnimation(0, _runAnimation, true);
        }
    
        private void PlayIdleAnimation() => 
            _animationState.SetAnimation(0, _idleAnimation, true);
    
        private void PlayHoveringAnimation() => 
            _animationState.SetAnimation(0, _hoverAnimation, true);

        private void SetLeftPointing() =>
            _animationSkeleton.ScaleX = -1;
    
        private void SetRightPointing() =>
            _animationSkeleton.ScaleX = 1;
    }
}