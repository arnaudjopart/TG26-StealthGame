using System;
using UnityEngine;

namespace _Project.Scripts.Views.Interface
{
    public interface ICanBeDetected
    {
        event Action OnDestroy;
        Vector3 Position { get; }
        public void ReactToDetection();
    }
}