using System;
using System.Collections.Generic;
using _Project.Scripts.Views.Interface;
using UnityEngine;
using UnityEngine.AI;

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

        private void Awake()
        {
            _detection = GetComponent<IDetectPlayer>();
            _move = GetComponent<IMove>();
            _detection.OnTriggerEnterEvent+= DetectionOnOnTriggerEvent;
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
                            canBeDetected.OnDestroy+= CanBeDetectedOnOnDestroy;
                        }
                        else
                        {
                            throw new Exception("Can't add " + obj + " to " + canBeDetected);
                        }
                    }
                    else
                    {
                        canBeDetected.OnDestroy -= CanBeDetectedOnOnDestroy;
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

        private void CanBeDetectedOnOnDestroy()
        {
            throw new NotImplementedException();
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
                    
                    _move.SetMoveDestination(_target.Position);
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
            _state = State.Pursuit;
        }
    }
}