using System;
using _Project.Scripts.Views.Interface;
using UnityEngine;

namespace _Project.Scripts.Views
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public class RootMotionBasedPlayerView : MonoBehaviour, IPlayerView, ITakeCover
    {
        private static readonly int BlendFloat = Animator.StringToHash("WalkBlendFloat");
        private static readonly int IsCoverBool = Animator.StringToHash("isCoverBool");
        private CharacterController _characterController;
        private Animator _animator;
        [SerializeField] private float _currentSpeed;
        [SerializeField] private float _velocity;
        [SerializeField] private float _smoothTime;
        private Vector3 _leaveCover;
       
        public event Action OnLeaveCoverEvent;
        public void EndCover()
        {
            _animator.applyRootMotion = true;
            _animator.SetBool(IsCoverBool,false);
        }

        private void Awake()
        {
            _animator =  GetComponent<Animator>();
            _characterController =  GetComponent<CharacterController>();
        }

        public void Move(Vector3 direction, float moveSpeed)
        {
            if (direction != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = rotation;
            }
            
            _currentSpeed = Mathf.SmoothDamp(_currentSpeed, direction.magnitude, ref _velocity, _smoothTime);
            _animator.SetFloat(BlendFloat, _currentSpeed);
            var deltaMove = _animator.deltaPosition;
            _characterController.SimpleMove(deltaMove);
        }

        public void CoverMove(Vector3 data)
        {
            var projectedMoveData = Vector3.ProjectOnPlane(data, _leaveCover);
            var rightVector = Vector3.Cross(Vector3.down, _leaveCover);
            var moveDirection = Mathf.Sign(Vector3.Dot(projectedMoveData, rightVector));
            
            _currentSpeed = Mathf.SmoothDamp(_currentSpeed, projectedMoveData.magnitude, ref _velocity, _smoothTime);
            var clampDeltaMove = rightVector * (_currentSpeed * moveDirection);
            _animator.applyRootMotion = false;
            _animator.SetFloat(BlendFloat, _currentSpeed*moveDirection);
            _characterController.SimpleMove(clampDeltaMove);
            if (Vector3.Dot(_leaveCover, data.normalized) > 0.9f)
            {
                OnLeaveCoverEvent?.Invoke();
            }
        }

        public void TakeCover(Vector3 direction)
        {
            if (direction == Vector3.zero) return;
            _leaveCover  = direction;
            var rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
            _animator.SetBool("isCoverBool",true);
            _currentSpeed = 0;
        }
    }
}