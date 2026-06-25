using _Project.Scripts.Views.Interface;
using UnityEngine;

namespace _Project.Scripts.Presenters
{
    public class CameraSystem
    {
        private readonly ICameraProvider[] _levelCameras;
        private readonly IUpdateInputAxis _playerInputListener;
        private Transform _currentCameraTransform;
        private Transform _defaultCamera;

        public CameraSystem(IUpdateInputAxis playerInputListener, ICameraProvider[] levelCameras)
        {
            _playerInputListener = playerInputListener;
            _levelCameras = levelCameras;
        
            foreach (var cameraProvider in _levelCameras)
            {
                cameraProvider.OnCameraProvideEvent+= OnCameraProvideEvent;
                cameraProvider.OnCameraReleaseEvent+= CameraReleaseEvent;
            }
        }

        public void SetDefaultCamera(Transform defaultCamera)
        {
            _defaultCamera = defaultCamera;
            _currentCameraTransform = _defaultCamera;
        }

        private void CameraReleaseEvent(Transform obj)
        {
            _currentCameraTransform.gameObject.SetActive(false);
            _currentCameraTransform = obj==null ? _defaultCamera : obj;
            _playerInputListener.SwitchAxis(_currentCameraTransform);
            _currentCameraTransform.gameObject.SetActive(true);
        }

        private void OnCameraProvideEvent(Transform obj)
        {
            _currentCameraTransform.gameObject.SetActive(false);
            _playerInputListener.SwitchAxis(obj);
            _currentCameraTransform= obj.transform;
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
