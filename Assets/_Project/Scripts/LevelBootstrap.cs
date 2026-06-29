using _Project.Scripts.Models;
using _Project.Scripts.Presenters;
using _Project.Scripts.Views.Interface;
using Unity.Cinemachine;
using UnityEngine;


namespace _Project.Scripts
{
    public class LevelBootstrap : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField, Tooltip("Level Start Position")] private Transform _playerSpawnPoint;
        [SerializeField, Tooltip("The main camera")] CinemachineCamera _mainCamera;
        private PlayerMovementInputPresenter _playerMovementInputPresenter;
        private GameObject _playerInstance;
        [SerializeField] private Transform[] _cameraList;
        private int _index;
        [SerializeField] private GameObject[] _levelCameras;
        private WallDetectionPresenter _wallDetectionPresenter;
        private GameplayInputManager _inputManager;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            _playerInstance = Instantiate(_playerPrefab, _playerSpawnPoint.position,Quaternion.identity);
            _mainCamera.Target = new CameraTarget()
            {
                LookAtTarget = _playerInstance.transform,
                TrackingTarget = _playerInstance.transform
            };
            
            var playerMovementData = new PlayerMovementModel();
            
            _inputManager = new GameplayInputManager(_playerInstance, playerMovementData);
            _wallDetectionPresenter = new WallDetectionPresenter(_playerInstance.GetComponent<IWallDetectionView>(),_playerInstance.GetComponent<ITakeCover>(), playerMovementData);
            
            _wallDetectionPresenter.OnWallDetectedEvent += _inputManager.SwitchToSneakyInput;
            _wallDetectionPresenter.OnLeaveCoverEvent += _inputManager.SwitchToDefaultInput;
            
            var cameraSystem = new CameraSystem(_levelCameras.ExtractInterface<ICameraProvider>());
            cameraSystem.OnCameraSwitchEvent += _inputManager.UpdateInputAxis;
            cameraSystem.SetDefaultCamera(_mainCamera.transform);
            
            _inputManager.SwitchToDefaultInput();
        }

        // Update is called once per frame
        private void Update()
        {
            _inputManager.CurrentTickableController.Tick(Time.deltaTime);
            _wallDetectionPresenter.Tick(Time.deltaTime);
        }
    }
}