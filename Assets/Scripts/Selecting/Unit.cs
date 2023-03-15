using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 destination)
    {
        _agent.isStopped = false;
        _agent.SetDestination(destination);
    }

    public void OnStop()
    {
        _agent.isStopped = true;
        _agent.ResetPath();
    }

    public bool HasReachedDestination()
    {
        return _agent.velocity == Vector3.zero;
    }
}
