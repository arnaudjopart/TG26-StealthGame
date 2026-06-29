using _Project.Scripts.Views;
using _Project.Scripts.Views.Interface;
using UnityEngine;

namespace _Project.Scripts.Presenters
{
    public class SneakyInputPresenter: IInputTickable
    {
        private readonly ISneakyInputProvider _sneakyInput;
        private readonly IPlayerView _player;

        public SneakyInputPresenter(ISneakyInputProvider sneakyInput, IPlayerView player)
        {
            _sneakyInput = sneakyInput;
            _player = player;
        }

        public void Tick(float deltaTime)
        {
            var data = _sneakyInput.MoveDirection;
            _player.CoverMove(data);
        }
    }

    public abstract class InputPresenterBase : IInputTickable
    {
        public abstract void Tick(float deltaTime);
    }
}