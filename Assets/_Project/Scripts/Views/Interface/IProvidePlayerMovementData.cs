using UnityEngine;

namespace _Project.Scripts.Views.Interface
{
    public interface IProvidePlayerMovementData
    {
        Vector3 MoveDirection { get; }
        float MoveSpeed { get; }
    }
}