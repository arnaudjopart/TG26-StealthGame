using System;
using _Project.Scripts.Views.Interface;
using UnityEngine;

namespace _Project.Scripts.Views
{
    public class WallDetectionSystem : MonoBehaviour, IWallDetectionView
    {
        [SerializeField] private float _wallDetectionDistance;
        [SerializeField, Tooltip("Vertical offset of the wall detection ray")] private float _offset=.5f;
        private Ray _ray;
        [SerializeField,Tooltip("Layers interacting with Wall Detection")]private LayerMask _layerMask;
        private float _validationTimer;
        [SerializeField] private float _coverTimeValidation = .5f;
        public event Action<Vector3> OnCoverEvent;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _ray = new Ray();
        }

        // Update is called once per frame
        void Update()
        {
            _ray.origin = transform.position+Vector3.up*_offset;
            _ray.direction = transform.forward;
            if (Physics.Raycast(_ray,  out var hit, _wallDetectionDistance, _layerMask))
            {
                _validationTimer+=Time.deltaTime;
                if (_validationTimer >= _coverTimeValidation)
                {
                    OnCoverEvent?.Invoke(hit.normal);
                }
                Debug.Log(hit.transform.name);
            }else
            {
                _validationTimer = 0f;
            }
        }

        private void OnDrawGizmos()
        {
            var debugRay = new Ray
            {
                origin = transform.position+Vector3.up*_offset,
                direction = transform.forward
            };
            Debug.DrawRay(debugRay.origin, debugRay.direction * _wallDetectionDistance, Color.red);
        }
    }
}
