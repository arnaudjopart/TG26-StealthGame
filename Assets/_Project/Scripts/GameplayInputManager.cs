using System;
using System.Collections.Generic;
using _Project.Scripts.Models;
using _Project.Scripts.Presenters;
using _Project.Scripts.Views;
using _Project.Scripts.Views.Interface;
using UnityEngine;

namespace _Project.Scripts
{
    internal class GameplayInputManager
    {
        private readonly PlayerActionsListener _defaultInput;
        private readonly SneakyInputListener _sneakyInput;
        private readonly PlayerMovementInputPresenter _defaultInputController;
        private readonly SneakyInputPresenter _sneakyInputPresenter;
        
        private readonly List<IInputListener> _inputListeners;
        private IInputListener _currentInputListener;

        public GameplayInputManager(GameObject player, PlayerMovementModel playerMovementModel)
        {
            _inputListeners = new List<IInputListener>();
            _defaultInput = new PlayerActionsListener(Camera.main.transform);
            _defaultInputController = new PlayerMovementInputPresenter(_defaultInput, player.GetComponent<IPlayerView>(), playerMovementModel);
            _sneakyInput = new SneakyInputListener(Camera.main.transform);
            _sneakyInputPresenter = new SneakyInputPresenter(_sneakyInput, player.GetComponent<IPlayerView>(), playerMovementModel);
            
            _inputListeners.Add(_defaultInput);
            _inputListeners.Add(_sneakyInput);
        }

        public void SwitchToDefaultInput()
        {
            CurrentTickableController = _defaultInputController;
            Toggle(_defaultInput, true);
        }

        public IInputTickable CurrentTickableController { get; private set; }

        public void SwitchToSneakyInput()
        {
            Toggle(_sneakyInput, true);
            CurrentTickableController = _sneakyInputPresenter;
        }
        
        private void Toggle(IInputListener input, bool b)
        {
            foreach (var inputListener in _inputListeners)
            {
                inputListener.Enable(inputListener == input && b);
            }
            _currentInputListener = input;
        }

        public void UpdateInputAxis(Transform obj)
        {
            _currentInputListener.SwitchAxis(obj);
        }
    }
}