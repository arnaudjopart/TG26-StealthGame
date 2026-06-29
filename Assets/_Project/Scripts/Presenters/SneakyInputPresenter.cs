using _Project.Scripts.Models;
using _Project.Scripts.Views;
using _Project.Scripts.Views.Interface;
using UnityEngine;

namespace _Project.Scripts.Presenters
{
    public class SneakyInputPresenter: IInputTickable
    {
        private readonly ISneakyInputProvider _sneakyInput;
        private readonly IPlayerView _player;
        private readonly PlayerMovementModel _playerMovementModel;

        public SneakyInputPresenter(ISneakyInputProvider sneakyInput, IPlayerView player, PlayerMovementModel playerMovementModel)
        {
            _sneakyInput = sneakyInput;
            _player = player;
            _playerMovementModel = playerMovementModel;
        }

        public void Tick(float deltaTime)
        {
            var data = _sneakyInput.MoveDirection;
            _player.CanMoveLeft = _playerMovementModel.CanMoveLeft;
            _player.CanMoveRight = _playerMovementModel.CanMoveRight;
            _player.CoverMove(data);
        }
    }

    public abstract class InputPresenterBase : IInputTickable
    {
        public abstract void Tick(float deltaTime);
    }
}