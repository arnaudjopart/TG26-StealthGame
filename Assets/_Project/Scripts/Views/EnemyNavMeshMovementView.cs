using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
public class EnemyNavMeshMovementView : MonoBehaviour, IMove
{
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMoveDestination(Vector3 destination)
    {
        _navMeshAgent.SetDestination(destination);
        _animator.SetFloat("Blend", _navMeshAgent.velocity.magnitude);
    }

    private void OnAnimatorMove()
    {
        
        var rootPosition = _animator.rootPosition;
        _navMeshAgent.nextPosition = rootPosition;
        transform.position = rootPosition;
    }
}

public interface IMove
{
    void SetMoveDestination(Vector3 destination);
}
