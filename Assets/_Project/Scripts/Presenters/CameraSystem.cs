using System;
using _Project.Scripts.Views.Interface;
using UnityEngine;

namespace _Project.Scripts.Presenters
{
    public class CameraSystem
    {
        private readonly ICameraProvider[] _levelCameras;
        private Transform _currentCameraTransform;
        private Transform _defaultCamera;

        public CameraSystem(ICameraProvider[] levelCameras)
        {

            _levelCameras = levelCameras;
        
            foreach (var cameraProvider in _levelCameras)
            {
                cameraProvider.OnCameraProvideEvent+= OnCameraProvideEvent;
                cameraProvider.OnCameraReleaseEvent+= CameraReleaseEvent;
            }
        }

        public event Action<Transform> OnCameraSwitchEvent;

        public void SetDefaultCamera(Transform defaultCamera)
        {
            _defaultCamera = defaultCamera;
            _currentCameraTransform = _defaultCamera;
        }

        private void CameraReleaseEvent(Transform obj)
        {
            _currentCameraTransform.gameObject.SetActive(false);
            _currentCameraTransform = obj==null ? _defaultCamera : obj;
            OnCameraSwitchEvent?.Invoke(_currentCameraTransform);//SwitchAxis(_currentCameraTransform);
            _currentCameraTransform.gameObject.SetActive(true);
        }

        private void OnCameraProvideEvent(Transform obj)
        {
            _currentCameraTransform.gameObject.SetActive(false);
            //_playerInputListener.SwitchAxis(obj);
            _currentCameraTransform= obj.transform;
            OnCameraSwitchEvent?.Invoke(_currentCameraTransform);
            _currentCameraTransform.gameObject.SetActive(true);
        }

        // Update is called once per frame
        void OnDestroy()
        {
            foreach (var provider in _levelCameras)
            {
                provider.OnCameraProvideEvent-= OnCameraProvideEvent;
            }
        }
    }
}
