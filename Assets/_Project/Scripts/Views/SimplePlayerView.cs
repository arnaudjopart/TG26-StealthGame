using _Project.Scripts.Views.Interface;
using UnityEngine;

namespace _Project.Scripts.Views
{
    [RequireComponent(typeof(CharacterController))]
    public class SimplePlayerView : MonoBehaviour, IPlayerView
    {
        private CharacterController _characterController;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Move(Vector3 normalizedMoveData, float moveSpeed)
        {
            Quaternion rotation = Quaternion.LookRotation(normalizedMoveData);
            transform.rotation = rotation;
            _characterController.SimpleMove(transform.forward * moveSpeed);
        }

        public void CoverMove(Vector3 data)
        {
            throw new System.NotImplementedException();
        }

        public bool CanMoveLeft { get; set; }
        public bool CanMoveRight { get; set; }
    }
}
