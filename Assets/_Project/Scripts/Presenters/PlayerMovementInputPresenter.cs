using _Project.Scripts.Views.Interface;
using UnityEngine;

namespace _Project.Scripts.Presenters
{
    public class PlayerMovementInputPresenter
    {
        private readonly IProvidePlayerMovementData _inputListener;
        private readonly IPlayerView _playerView;
        private readonly PlayerMovementModel _playerMovementModel;

        public PlayerMovementInputPresenter(
            IProvidePlayerMovementData providePlayerInputListener,
            IPlayerView playerView, 
            PlayerMovementModel playerMovementModel)
        {
            _inputListener = providePlayerInputListener;
            _playerView = playerView;
            _playerMovementModel = playerMovementModel;
        }

        public void Tick(float deltaTime)
        {
            var speed = _inputListener.MoveSpeed * _playerMovementModel.MoveSpeed;
            _playerView.Move(_inputListener.MoveDirection, speed);
        }
    }
}