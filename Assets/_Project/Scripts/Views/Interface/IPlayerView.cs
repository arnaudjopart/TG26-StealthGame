using UnityEngine;

namespace _Project.Scripts.Views.Interface
{
    public interface IPlayerView
    {
        void Move(Vector3 normalizedMoveData, float moveSpeed);
    }
}