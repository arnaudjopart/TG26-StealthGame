using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace _Project.Scripts
{
    public class PlayableParameters : MonoBehaviour
    {
        [SerializeField] private TimelineAsset _timelineAsset;
        [SerializeField] private PlayableDirector _director;

        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private Animator _targetAnimator;
        [SerializeField] private Vector3 _positionOffset = Vector3.back*3;
        [SerializeField] private float _rotationOffset=22;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
           
        }

        [ContextMenu("Play")]
        public void Play()
        {
            var targetPosition = _targetAnimator.transform.position;
            var targetRotation = _targetAnimator.transform.rotation;
            var playerAnimationTrack = _timelineAsset.GetRootTrack(0) as AnimationTrack;
            var targetAnimationTrack = _timelineAsset.GetRootTrack(1) as AnimationTrack;
            if (targetAnimationTrack == null || playerAnimationTrack == null) return;
            _director.SetGenericBinding(playerAnimationTrack, _playerAnimator);
            _director.SetGenericBinding(targetAnimationTrack, _targetAnimator);

            var clips = playerAnimationTrack.GetClips().ToArray();
            var playerAnimationPlayableAsset = clips[0].asset as AnimationPlayableAsset;
            if (playerAnimationPlayableAsset == null) return;
            playerAnimationPlayableAsset.position = targetPosition+targetRotation*_positionOffset;
            playerAnimationPlayableAsset.rotation = targetRotation*Quaternion.Euler(0, _rotationOffset, 0);
            
            clips = targetAnimationTrack.GetClips().ToArray();
            var targetAnimationPlayableAsset = clips[0].asset as AnimationPlayableAsset;
            if (targetAnimationPlayableAsset == null) return;
            targetAnimationPlayableAsset.position = targetPosition;
            targetAnimationPlayableAsset.rotation = targetRotation;
            
            _director.Play();
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
