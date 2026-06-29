using System;
using _Project.Scripts.Views;
using _Project.Scripts.Views.Interface;
using UnityEngine;

namespace _Project.Scripts.Presenters
{
    public class WallDetectionPresenter
    {
        private readonly IWallDetectionView _wallDetectionView;
        private readonly ITakeCover _playerView;
        

        public WallDetectionPresenter(IWallDetectionView wallDetectionView, ITakeCover playerView)
        {
            _wallDetectionView = wallDetectionView;
            _playerView = playerView;
            _wallDetectionView.OnCoverEvent+= WallDetectionViewOnOnCoverEvent;
            _playerView.OnLeaveCoverEvent += LeaveCover;
        }

        private void LeaveCover()
        {
            _playerView.EndCover();
            OnLeaveCoverEvent?.Invoke();
        }

        public event Action OnWallDetectedEvent;
        public event Action OnLeaveCoverEvent;

        private void WallDetectionViewOnOnCoverEvent(Vector3 wallNormal)
        {
            OnWallDetectedEvent?.Invoke();
            _playerView.TakeCover(wallNormal);
        }
        
    }
}