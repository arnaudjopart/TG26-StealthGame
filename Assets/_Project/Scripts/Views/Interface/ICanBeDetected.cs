using System;
using UnityEngine;

namespace _Project.Scripts.Views.Interface
{
    public interface ICanBeDetected
    {
        event Action<GameObject> OnDestroyEvent;
        Vector3 Position { get; }
        public void ReactToDetection();
        void Apply();
    }
}