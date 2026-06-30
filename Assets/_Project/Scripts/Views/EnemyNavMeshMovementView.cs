using System;
using _Project.Scripts.Views.Interface;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Views
{
    [RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
    public class EnemyNavMeshMovementView : MonoBehaviour, IMove
    {
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private bool _isMovingToTarget;
        public event Action OnReachTargetEvent;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updatePosition = false;
            _navMeshAgent.updateRotation = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (_isMovingToTarget == false)
            {
                _navMeshAgent.isStopped = true;
                _navMeshAgent.velocity =  Vector3.zero;
                _animator.SetFloat("Blend", _navMeshAgent.velocity.magnitude);
                return;
            }
            if (_navMeshAgent.remainingDistance >0 && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                
                OnReachTargetEvent?.Invoke();
                _isMovingToTarget = false;
                
            }
        }

        public void SetMoveDestination(Vector3 destination)
        {
            _isMovingToTarget = true;
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(destination);
            _animator.SetFloat("Blend", _navMeshAgent.velocity.magnitude);
        }

        private void OnAnimatorMove()
        {
        
            var rootPosition = _animator.rootPosition;
            _navMeshAgent.nextPosition = rootPosition;
            transform.position = rootPosition;
        }
    }
}