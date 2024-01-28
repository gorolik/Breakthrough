using UnityEngine;
using System;

[RequireComponent(typeof(FieldOfView))]  
public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private SecurityBlock _securityBlock;
    [SerializeField] private Health _health;

    private bool _isActive = true;
    private Player _player;
    private FieldOfView _fieldOfView;

    public Action Deactivated;


    private void Awake()
    {
        _fieldOfView = GetComponent<FieldOfView>();
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        if(_securityBlock != null)
            _securityBlock.Deactivated += Deactivate;

        Enemy.EnemyDied += OnEnemyDied;
        _health.Die += Deactivate;
    }

    private void OnDisable()
    {
        if (_securityBlock != null)
            _securityBlock.Deactivated -= Deactivate;

        Enemy.EnemyDied -= OnEnemyDied;
        _health.Die -= Deactivate;
    }

    private void Update()
    {
        if (_player == null || _isActive == false)
            return;

        if (_fieldOfView.IsTargetInView(_player.ObservePoint, _player.ObserveTarget) == true)
            _securityBlock.DetectPlayer();
    }

    private void OnEnemyDied(Enemy enemy)
    {
        if (_isActive == false)
            return;

        if (_fieldOfView.IsTargetInView(enemy.ObservePoint, enemy.transform) == true)
            _securityBlock.DetectPlayer();
    }

    private void Deactivate()
    {
        if (_isActive == false)
            return;

        _isActive = false;

        Deactivated?.Invoke();
    }
}
