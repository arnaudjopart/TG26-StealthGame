using UnityEngine;

namespace _Project.Scripts.Views.Interface
{
    public interface IPlayerView
    {
        void Move(Vector3 direction, float moveSpeed);

        void CoverMove(Vector3 data);
        bool CanMoveLeft { get; set; }
        bool CanMoveRight { get; set; }
    }
}