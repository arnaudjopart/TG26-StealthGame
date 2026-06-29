using System;
using UnityEngine;

namespace _Project.Scripts.Views.Interface
{
    public interface IWallDetectionView
    {
        event Action<Vector3> OnCoverEvent;
        bool IsDetectingWallOnRightSide { get; }
        bool IsDetectingWallOnLeftSide { get; }
        void SwitchToCornerDetection();
        void SwitchToWallDetection();
        void ActiveRightCamera(bool active);
        void ActiveLeftCamera(bool active);
    }
}