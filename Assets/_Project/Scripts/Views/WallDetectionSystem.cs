using System;
using _Project.Scripts.Views.Interface;
using UnityEngine;

namespace _Project.Scripts.Views
{
    public class WallDetectionSystem : MonoBehaviour, IWallDetectionView
    {
        [Header("Wall Detection Settings")]
        [SerializeField] private float _wallDetectionDistance;
        [SerializeField, Tooltip("Vertical offset of the wall detection ray")] private float _offset=.5f;
        private Ray _ray;
        [SerializeField,Tooltip("Layers interacting with Wall Detection")]private LayerMask _layerMask;
        private float _validationTimer;
        [SerializeField] private float _coverTimeValidation = .5f;
        [Space(10), Header("CornerDetection Settings")]
        [SerializeField] private Transform _upperLeftCornerDetectionTransform;
        [SerializeField] private Transform _upperRightCornerDetectionTransform;
        [SerializeField] private float _cornerDetectionDistance=.3f;
        public event Action<Vector3> OnCoverEvent;
        public bool IsDetectingWallOnRightSide => _isOnCornerRight;
        public bool IsDetectingWallOnLeftSide => _isOnCornerLeft;

        private bool _isOnCornerRight;
        private bool _isOnCornerLeft;
        [SerializeField] private GameObject _rightCamera;
        [SerializeField] private GameObject _leftCamera;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }
        

        public void DetectCorners()
        {
            var upperLeftRay = new Ray(_upperLeftCornerDetectionTransform.position, transform.forward*-1);
            var upperRightRay = new Ray(_upperRightCornerDetectionTransform.position, transform.forward*-1);
                    
            _isOnCornerRight = Physics.Raycast(upperRightRay, _cornerDetectionDistance, _layerMask);
            _isOnCornerLeft = Physics.Raycast(upperLeftRay, _cornerDetectionDistance, _layerMask);

        }

        public void DetectWall()
        {
            var ray = new Ray
            {
                origin = transform.position+Vector3.up*_offset,
                direction = transform.forward
            };
            if (Physics.Raycast(ray,  out var hit, _wallDetectionDistance, _layerMask))
            {
                _validationTimer+=Time.deltaTime;
                if (_validationTimer >= _coverTimeValidation)
                {
                    OnCoverEvent?.Invoke(hit.normal);
                }

            }else
            {
                _validationTimer = 0f;
            }
        }

        public void SwitchToCornerDetection()
        {
            
        }

        public void SwitchToWallDetection()
        {
            ActiveLeftCamera(false);
            ActiveRightCamera(false);
        }

        public void ActiveLeftCamera(bool active)
        {
            if (_leftCamera && _leftCamera.activeInHierarchy == active) return;
            _leftCamera.SetActive(active);
        }
        
        public void ActiveRightCamera(bool active)
        {
            if (_rightCamera && _rightCamera.activeInHierarchy == active) return;
            _rightCamera.SetActive(active);
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
