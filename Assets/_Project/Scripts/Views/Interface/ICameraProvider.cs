using System;
using UnityEngine;

namespace _Project.Scripts.Views.Interface
{
    public interface ICameraProvider
    {
        Transform GetCameraTransform();
        event Action<Transform> OnCameraProvideEvent;
        event Action<Transform> OnCameraReleaseEvent;
    }
}