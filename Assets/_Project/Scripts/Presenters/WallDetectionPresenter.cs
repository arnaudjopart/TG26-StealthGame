using System;
using _Project.Scripts.Models;
using _Project.Scripts.Views;
using _Project.Scripts.Views.Interface;
using UnityEngine;

namespace _Project.Scripts.Presenters
{
    public class WallDetectionPresenter
    {
        private readonly IWallDetectionView _wallDetectionView;
        private readonly ITakeCover _playerView;
        private readonly PlayerMovementModel _playerMovementModel;

        private enum State
        {
            DetectingWall,
            DetectingCorner
        } 
        private State _state = State.DetectingWall;

        public WallDetectionPresenter(IWallDetectionView wallDetectionView, ITakeCover playerView, PlayerMovementModel playerMovementModel)
        {
            _wallDetectionView = wallDetectionView;
            _playerView = playerView;
            _playerMovementModel = playerMovementModel;
            _wallDetectionView.OnCoverEvent+= WallDetectionViewOnOnCoverEvent;
            _playerView.OnLeaveCoverEvent += LeaveCover;
        }

        private void LeaveCover()
        {
            _playerView.EndCover();
            _wallDetectionView.SwitchToWallDetection();
            OnLeaveCoverEvent?.Invoke();
            _state = State.DetectingWall;
        }

        public event Action OnWallDetectedEvent;
        public event Action OnLeaveCoverEvent;

        private void WallDetectionViewOnOnCoverEvent(Vector3 wallNormal)
        {
            OnWallDetectedEvent?.Invoke();
            _playerView.TakeCover(wallNormal);
            _wallDetectionView.SwitchToCornerDetection();
            _state = State.DetectingCorner;
        }

        public void Tick(float deltaTime)
        {
            switch (_state)
            {
                case State.DetectingWall:
                    break;
                case State.DetectingCorner:
                    _playerMovementModel.CanMoveRight = _wallDetectionView.IsDetectingWallOnRightSide;
                    _playerMovementModel.CanMoveLeft = _wallDetectionView.IsDetectingWallOnLeftSide;
                    _wallDetectionView.ActiveLeftCamera(!_playerMovementModel.CanMoveLeft);
                    _wallDetectionView.ActiveRightCamera(!_playerMovementModel.CanMoveRight);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
    }
}