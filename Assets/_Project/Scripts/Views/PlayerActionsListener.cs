using _Project.Scripts.Views.Interface;
using ajc.script.input;
using UnityEngine;
using UnityEngine.InputSystem;
// ReSharper disable PossiblyImpureMethodCallOnReadonlyVariable

namespace _Project.Scripts.Views
{
    public class PlayerActionsListener: PlayerInputSystem.IPlayerActions, IProvidePlayerMovementData, IInputListener
    {
        private Transform _axisTransform;
        private Vector2 _moveVector;
        private readonly PlayerInputSystem.PlayerActions _actionMap;
        private Vector3 _projectedForwardVector;
        private Vector3 _projectedRightVector;
        private float _speed;

        public PlayerActionsListener(Transform axisTransform)
        {
            _axisTransform = axisTransform;
            var inputSystem = new PlayerInputSystem();
            _actionMap = inputSystem.Player;
            _actionMap.AddCallbacks(this);
        }

        public void SwitchAxis(Transform axisReference)
        {
            _axisTransform =  axisReference;
        }
        public void Enable(bool enable)
        {
            if(enable) _actionMap.Enable();
            else _actionMap.Disable();
        }



        public void Toggle()
        {
            Enable(_actionMap.enabled);

        }
        private void Dispose()
        {
            _actionMap.RemoveCallbacks(this);
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

        public void OnLook(InputAction.CallbackContext context)
        {
        
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
        
        }
    

        public void OnInteract(InputAction.CallbackContext context)
        {
            Debug.Log("Interact");
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnNext(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public Vector3 MoveDirection  => (_projectedForwardVector*_moveVector.y+_projectedRightVector*_moveVector.x);
        public float MoveSpeed => _speed;
    }
}
