using _Project.Scripts.Presenters;
using _Project.Scripts.Views;
using _Project.Scripts.Views.Interface;
using Unity.Cinemachine;
using Unity.VisualScripting.IonicZip;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts
{
    public class LevelBootstrap : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField, Tooltip("Level Start Position")] private Transform _playerSpawnPoint;
        [SerializeField, Tooltip("The main camera")] CinemachineCamera _mainCamera;
        private PlayerActionsListener _playerInputListener;
        private PlayerMovementInputPresenter _playerMovementInputPresenter;
        private GameObject _playerInstance;
        [SerializeField] private Transform[] _cameraList;
        private int _index;
        [SerializeField] private GameObject[] _levelCameras;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            _playerInstance = Instantiate(_playerPrefab, _playerSpawnPoint.position,Quaternion.identity);
            _mainCamera.Target = new CameraTarget()
            {
                LookAtTarget = _playerInstance.transform,
                TrackingTarget = _playerInstance.transform
            };
            
            _playerInputListener = new PlayerActionsListener(Camera.main.transform);
            _playerInputListener.Enable(true);

            var playerMovementData = new PlayerMovementModel();
            _playerMovementInputPresenter = new PlayerMovementInputPresenter(
                _playerInputListener, 
                _playerInstance.GetComponent<IPlayerView>(),
                playerMovementData);
            var cameraSystem = new CameraSystem(_playerInputListener, _levelCameras.ExtractInterface<ICameraProvider>());
            cameraSystem.SetDefaultCamera(_mainCamera.transform);
        }

        // Update is called once per frame
        void Update()
        {
            _playerMovementInputPresenter.Tick(Time.deltaTime);
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                _cameraList[_index].gameObject.SetActive(false);
                _index++;
                _index %= _cameraList.Length;
                _cameraList[_index].gameObject.SetActive(true);
                _playerInputListener.SwitchAxis(_cameraList[_index]);
            }
        }
    }
}