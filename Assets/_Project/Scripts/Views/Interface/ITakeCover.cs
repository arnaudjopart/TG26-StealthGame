using System;
using UnityEngine;

namespace _Project.Scripts.Views.Interface
{
    public interface ITakeCover
    {
        void TakeCover(Vector3 direction);
        event Action OnLeaveCoverEvent;
        void EndCover();
    }
}