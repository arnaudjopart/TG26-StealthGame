using System;
using UnityEngine;

namespace _Project.Scripts.Views.Interface
{
    public interface IMove
    {
        void SetMoveDestination(Vector3 destination);
        event Action OnReachTargetEvent;
    }
}