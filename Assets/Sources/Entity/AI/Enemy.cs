using UnityEngine;
using System;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SecurityBlock[] _observerBlocks;
    [SerializeField] private FieldOfView _fieldOfView;
    [SerializeField] private Transform _observePoint;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private AIMover _mover;
    [SerializeField] private Health _health;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _alternativeAttackDistance;
    [SerializeField] private AIHand Hand;
    [SerializeField] private bool _alternativeUse;
    [SerializeField] private float _minAlternativeUseDistance = 3.5f;

    private Player _player;
    private bool _isActive = true;
    private bool _seePlayer;
    private float _defaultStoppingDistance;

    public delegate void Died(Enemy enemy);
    public static Died EnemyDied;

    public Action PlayerDetected;
    public Action ProceedDie;

    public Transform PlayerObservePoint => _player.ObservePoint;
    public Transform ObservePoint => _observePoint;


    protected void Start()
    {
        _defaultStoppingDistance = _agent.stoppingDistance;
        _player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        _health.Die += OnDie;
        EnemyDied += OnEnemyDied;

        foreach (var block in _observerBlocks)
        {
            block.PlayerDetecteded += OnPlayerDetected;
        }
    }

    private void OnDisable()
    {
        _health.Die -= OnDie;
        EnemyDied -= OnEnemyDied;

        foreach (var block in _observerBlocks)
        {
            block.PlayerDetecteded -= OnPlayerDetected;
        }
    }

    private void Update()
    {
        if (_isActive == false || _player == null)
            return;

        if (_fieldOfView.IsTargetInView(_player.ObservePoint, _player.ObserveTarget) == true)
        {
            _seePlayer = true;
            TryAttack();
        }
        else if(_seePlayer == true)
        {
            _seePlayer = false;
            OnPLayerLost();
        }
    }

    private void TryAttack()
    {
        _agent.stoppingDistance = _attackDistance;
        RunToPlayer();

        float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        if (distanceToPlayer <= _attackDistance || (_alternativeUse == true && distanceToPlayer < _minAlternativeUseDistance))
        {
            transform.LookAt(_player.transform);

            _agent.stoppingDistance = _attackDistance;
            Hand.UseItem();
        }
        else if(_alternativeUse == true)
        {
            _agent.stoppingDistance = _alternativeAttackDistance;
            Hand.AlternativeUseItem();
        }
    }

    private void OnPLayerLost()
    {
        _agent.stoppingDistance = _defaultStoppingDistance;
    }

    private void OnDie()
    {
        _isActive = false;
        _mover.StopMoving();

        EnemyDied?.Invoke(this);
        ProceedDie?.Invoke();
    }

    private void OnEnemyDied(Enemy enemy)
    {
        if (enemy == this)
            return;

        if (_mover.Status != AIMover.State.RunToTarget && _fieldOfView.IsTargetInView(enemy.ObservePoint, enemy.transform) == true)
            RunToPlayer();
    }

    private void OnPlayerDetected()
    {
        if(_mover.Status != AIMover.State.RunToTarget)
            RunToPlayer();
    }

    private void RunToPlayer()
    {
        if (_mover.Status != AIMover.State.RunToTarget)
            PlayerDetected?.Invoke();

        _mover.StartRunToPoint(_player.transform.position);
    }
}
