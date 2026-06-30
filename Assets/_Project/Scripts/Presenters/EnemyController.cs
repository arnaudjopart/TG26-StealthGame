using System;
using System.Collections.Generic;
using _Project.Scripts.Views.Interface;
using UnityEngine;

namespace _Project.Scripts.Presenters
{
    public class EnemyController : MonoBehaviour
    {
        private readonly Dictionary<EntityId, ICanBeDetected> _enemies = new Dictionary<EntityId, ICanBeDetected>();
        private IDetectPlayer _detection;
        [SerializeField] float _sightAngle =90;
        private ICanBeDetected _target;

        private enum State
        {
            Detect,
            Pursuit
        }
        
        
        private State _state =  State.Detect;
        private IMove _move;
        private Vector3 _lastPosition;

        private void Awake()
        {
            _detection = GetComponent<IDetectPlayer>();
            _move = GetComponent<IMove>();
            _detection.OnTriggerEnterEvent+= DetectionOnOnTriggerEvent;
            _move.OnReachTargetEvent+= MoveOnOnReachTargetEvent;
        }

        private void MoveOnOnReachTargetEvent()
        {
            if (_target != null)
            {
                _target.Apply();
                _target = null;
            }
        }

        private void DetectionOnOnTriggerEvent(GameObject obj, bool b)
        {
            try
            {
                var canBeDetected = obj.GetComponent<ICanBeDetected>();
                if (canBeDetected != null)
                {
                    if (b)
                    {
                        bool success = _enemies.TryAdd(obj.GetEntityId(), canBeDetected);
                        if (success)
                        {
                            Debug.Log("Successfully detected " + obj.GetEntityId());
                            canBeDetected.OnDestroyEvent+= CanBeDetectedOnOnDestroy;
                        }
                        else
                        {
                            throw new Exception("Can't add " + obj + " to " + canBeDetected);
                        }
                    }
                    else
                    {
                        canBeDetected.OnDestroyEvent -= CanBeDetectedOnOnDestroy;
                        _enemies.Remove(obj.GetEntityId());
                    }
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            catch (NullReferenceException e)
            {
                Debug.LogError(e.Message);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            
    
        }

        private void CanBeDetectedOnOnDestroy(GameObject obj)
        {
            Debug.Log(obj.GetEntityId());
            _enemies.Remove(obj.GetEntityId());
        }

        private void Update()
        {
            switch (_state)
            {
                case State.Detect:
                    foreach (var VARIABLE in _enemies)
                    {
                        if (_detection.IsThisTargetInSight(VARIABLE.Value.Position, _sightAngle, 5))
                        {
                            Detect(VARIABLE.Value);
                        };
                    }
                    break;
                case State.Pursuit:
                    if(_target != null) _move.SetMoveDestination(_target.Position);
                    else _state = State.Detect;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            


        }

        

        private void Detect(ICanBeDetected canBeDetected)
        {
            canBeDetected.ReactToDetection();
            _target = canBeDetected;
            SwitchToPursuit();
        }

        private void SwitchToPursuit()
        {
            _lastPosition = transform.position;
            _state = State.Pursuit;
        }
    }
}