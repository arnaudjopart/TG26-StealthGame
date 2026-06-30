using System;
using _Project.Scripts.Views.Interface;
using UnityEngine;

namespace _Project.Scripts.Views
{
    public class EnemyDetectionSystemView : MonoBehaviour, IDetectPlayer
    {
        public event Action<GameObject, bool> OnTriggerEnterEvent;
        [SerializeField, Tooltip("Head transform to set forward direction")] private Transform _headTransform;
        [SerializeField] private LayerMask _obstacleLayerMask;
        private Vector3 _targetDirection;
        private Vector3 _targetPosition;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEnterEvent?.Invoke(other.gameObject, true);
        }
        private void OnTriggerExit(Collider other)
        {
            OnTriggerEnterEvent?.Invoke(other.gameObject, false);
        }

        public bool IsThisTargetInSight(Vector3 position, float sightAngle, float distance)
        {
            _targetPosition = position;
            _targetDirection = (position - _headTransform.position).normalized;
            if (Vector3.Angle(_headTransform.forward, _targetDirection) > sightAngle) return false;
            var ray = new Ray(_headTransform.position, _targetDirection);
            return !Physics.Raycast(ray, distance, _obstacleLayerMask);
        }
        public bool IsCloseEnoughToBeHeard(GameObject player, float distance)
        {
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_headTransform.position, _targetPosition);
        }
    }
    
}
