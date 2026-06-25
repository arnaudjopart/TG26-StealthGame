using System;
using _Project.Scripts.Views.Interface;
using UnityEngine;

namespace _Project.Scripts.Views
{
    public abstract class CameraProviderBase : MonoBehaviour, ICameraProvider
    {
        public abstract Transform GetCameraTransform();
        public event Action<Transform> OnCameraProvideEvent;
        public event Action<Transform> OnCameraReleaseEvent;

        protected void InvokeCameraProvideEvent(Transform cameraTransform)
        {
            OnCameraProvideEvent?.Invoke(cameraTransform);
        }

        protected void InvokeCameraReleaseEvent(Transform cameraTransform)
        {
            OnCameraReleaseEvent?.Invoke(cameraTransform);
        }
    }
}