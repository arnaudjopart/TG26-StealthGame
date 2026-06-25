using System;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Views
{
    internal class OnTriggerEnterCameraProvider : CameraProviderBase
    {
        [SerializeField] private CinemachineCamera _cameraList;
        public override Transform GetCameraTransform()
        {
            throw new NotImplementedException();
        }

        private void OnTriggerEnter(Collider other)
        {
            InvokeCameraProvideEvent(_cameraList.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            InvokeCameraReleaseEvent(null);
        }
    }
}