using UnityEngine;
using System;

public class SecurityBlock : MonoBehaviour
{
    [SerializeField] private Health _health;

    private bool _isActive = true;

    public Action PlayerDetecteded;
    public Action Deactivated;


    private void OnEnable()
    {
        _health.Die += OnWiresBroked;
    }

    private void OnDisable()
    {
        _health.Die -= OnWiresBroked;
    }

    public void DetectPlayer()
    {
        if (_isActive == false)
            return;

        PlayerDetecteded?.Invoke();
    }

    private void OnWiresBroked()
    {
        if (_isActive == false)
            return;

        _isActive = false;

        Deactivated?.Invoke();
    }
}
