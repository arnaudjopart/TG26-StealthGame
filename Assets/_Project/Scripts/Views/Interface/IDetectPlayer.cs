using System;
using UnityEngine;

namespace _Project.Scripts.Views.Interface
{
    public interface IDetectPlayer
    {
        event Action<GameObject, bool>  OnTriggerEnterEvent;
        bool IsThisTargetInSight(Vector3 position, float sightAngle, float distance);
        bool IsCloseEnoughToBeHeard(GameObject player, float distance);
    }
}