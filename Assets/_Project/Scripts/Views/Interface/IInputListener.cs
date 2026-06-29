

using UnityEngine;

namespace _Project.Scripts.Views.Interface
{
    public interface IInputListener
    {
        void Enable(bool enable);
        void SwitchAxis(Transform axis);
    }
}