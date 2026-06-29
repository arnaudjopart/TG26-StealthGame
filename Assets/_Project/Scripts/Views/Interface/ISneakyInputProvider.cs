using UnityEngine;

namespace _Project.Scripts.Views.Interface
{
    public interface ISneakyInputProvider
    {
        Vector3 MoveDirection { get; }
        float MoveSpeed { get; }
    }
}