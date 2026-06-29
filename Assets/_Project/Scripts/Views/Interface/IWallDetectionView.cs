using System;
using UnityEngine;

namespace _Project.Scripts.Views.Interface
{
    public interface IWallDetectionView
    {
        event Action<Vector3> OnCoverEvent;
    }
}