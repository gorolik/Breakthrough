using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIMover : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _standTime = 6;
    [SerializeField] private float _walkSpeed = 3;
    [SerializeField] private float _runSpeed = 10;

    private const float _stopDistanceError = 0.5f;
    private int _currentPatrolPoint;
    private Coroutine _patrol;
    private Coroutine _running;

    public enum State { Stand, Patrol, RunToTarget }
    private State _state;

    public State Status => _state;


    private void Start()
    {
        StartPatrol();
    }

    public void StopMoving()
    {
        StopRunToPoint();
        StopPatrol();
    }

    public void StartPatrol()
    {
        if (_patrolPoints == null || _patrolPoints.Length <= 0)
            return;

        StopMoving();
        _agent.speed = _walkSpeed;
        _patrol = StartCoroutine(Patrol());
    }

    public void StopPatrol()
    {
        if (_patrol == null)
            return;

        StopCoroutine(_patrol);
        _patrol = null;
    }

    public void StartRunToPoint(Vector3 point)
    {
        _agent.speed = _runSpeed;
        StopMoving();
        _state = State.RunToTarget;
        _running = StartCoroutine(RunToPoint(point));
    }

    public void StopRunToPoint()
    {
        if (_running == null)
            return;

        StopCoroutine(_running);
        _running = null;
    }

    private IEnumerator Patrol()
    {
        bool inited = false;

        while (true)
        {
            if (Vector3.Distance(transform.position, _patrolPoints[_currentPatrolPoint].position) <= _agent.stoppingDistance + _stopDistanceError || inited == false || _agent.pathStatus == NavMeshPathStatus.PathInvalid || _agent.pathStatus == NavMeshPathStatus.PathPartial)
            {
                inited = true;
                _state = State.Stand;

                yield return new WaitForSeconds(_standTime);

                _state = State.Patrol;
                _currentPatrolPoint = GetNextPatrolPointInxed();
                _agent.SetDestination(_patrolPoints[_currentPatrolPoint].position);
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private int GetNextPatrolPointInxed()
    {
        if(_currentPatrolPoint + 1 >= _patrolPoints.Length)
        {
            _currentPatrolPoint = 0;
            return _currentPatrolPoint;
        }
        else
        {
            _currentPatrolPoint += 1;
            return _currentPatrolPoint;
        }
    }

    private IEnumerator RunToPoint(Vector3 point)
    {
        _agent.SetDestination(point);

        while (Vector3.Distance(transform.position, point) > _agent.stoppingDistance + _stopDistanceError)
        {
            yield return new WaitForEndOfFrame();
        }

        StartPatrol();
    }
}
