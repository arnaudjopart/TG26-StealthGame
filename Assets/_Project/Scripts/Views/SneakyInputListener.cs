using _Project.Scripts.Views.Interface;
using ajc.script.input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Views
{
    public class SneakyInputListener : PlayerInputSystem.ISneakingActions, IInputListener, ISneakyInputProvider
    {
        private readonly PlayerInputSystem _inputSystem;
        private Vector2 _moveVector;
        private int _speed;
        private Vector3 _projectedForwardVector;
        private Vector3 _projectedRightVector;
        private Transform _axisTransform;

        public SneakyInputListener(Transform mainTransform)
        { 
            _inputSystem = new PlayerInputSystem();
            _inputSystem.Sneaking.AddCallbacks(this);
            
            _axisTransform = mainTransform;
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Debug.Log("Update Axis: "+context.ReadValue<Vector2>());
                
                _moveVector = context.ReadValue<Vector2>();
                _speed = 1;
            }

            if (context.canceled)
            {
                _moveVector = Vector2.zero;
                _speed = 0;
                UpdateAxis();
            }
        }

        private void UpdateAxis()
        {
            _projectedForwardVector = Vector3.ProjectOnPlane(_axisTransform.forward, Vector3.up).normalized;
            _projectedRightVector = Vector3.ProjectOnPlane(_axisTransform.right, Vector3.up).normalized;

        }

        public void OnKnock(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void Enable(bool enable)
        {
            if(enable) _inputSystem.Sneaking.Enable();
            else _inputSystem.Sneaking.Disable();
        }

        public void SwitchAxis(Transform axis)
        {
            _axisTransform =  axis;
        }
        
        public Vector3 MoveDirection  => (_projectedForwardVector*_moveVector.y+_projectedRightVector*_moveVector.x);
        public float MoveSpeed => _speed;
    }
}