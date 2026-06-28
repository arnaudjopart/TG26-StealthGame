using System;
using _Project.Scripts.Views.Interface;
using UnityEngine;

namespace _Project.Scripts.Views
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public class RootMotionBasedPlayerView : MonoBehaviour, IPlayerView
    {
        private static readonly int BlendFloat = Animator.StringToHash("WalkBlendFloat");
        private CharacterController _characterController;
        private Animator _animator;
        [SerializeField] private float _currentSpeed;
        [SerializeField] private float _velocity;
        [SerializeField] private float _smoothTime;

        private void Awake()
        {
            _animator =  GetComponent<Animator>();
            _characterController =  GetComponent<CharacterController>();
        }

        public void Move(Vector3 direction, float moveSpeed)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            _currentSpeed = Mathf.SmoothDamp(_currentSpeed, direction.magnitude, ref _velocity, _smoothTime);
            _animator.SetFloat(BlendFloat, _currentSpeed);
            var deltaMove = _animator.deltaPosition;
            _characterController.SimpleMove(deltaMove);
            transform.rotation = rotation;
            
        }
    }
}