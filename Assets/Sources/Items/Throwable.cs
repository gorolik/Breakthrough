using System.Collections;
using UnityEngine;
using System;

public class Throwable : Weapon
{
    [SerializeField] private bool _canThrow = true;
    [SerializeField] private bool _returnAfterStoped = false;
    [SerializeField] private float _returnDelay;
    [SerializeField] private float _ricocheteAngle;
    [SerializeField] private float _maxRicochetesCount = 1;
    [SerializeField] private float _flySpeed;
    [SerializeField] private float _airDrag;
    [SerializeField] private float _gravity;

    private bool _canThrowAfterPickUp = true;
    private Hand _lastHand;
    private Coroutine _flyCoroutine;
    private Vector3 _previousPosition;
    private Collider _lastCollision;
    private bool _flying;

    private const float _throwDaleyAfterPickup = 0.27f;

    public Action Throwed;
    public Action Stuck;
    public Action Stoped;
    public Action Ricochete;


    private new void OnEnable()
    {
        base.OnEnable();

        Taked += OnTaked;
    }

    private new void OnDisable()
    {
        base.OnDisable();

        Taked -= OnTaked;
    }

    public override void AlternativeUse()
    {
        TryThrow();
    }

    public void TryThrow()
    {
        if (_flying != true && _canThrow == true && _canThrowAfterPickUp == true)
            StartFly();
    }

    private void OnTaked()
    {
        _lastCollision = null;
        _canThrowAfterPickUp = false;
        Invoke(nameof(AllowThrowAfterPickUp), _throwDaleyAfterPickup);
        Hand.TryGetComponent(out _lastHand);

        if (_flying == true)
            StopFly();
    }

    private void AllowThrowAfterPickUp()
    {
        _canThrowAfterPickUp = true;
    }

    private void StartFly()
    {
        Release();
        Vector3 startTarget = TargetDestinator.GetTarget(Mathf.Infinity, AttackTargets).point;
        _flyCoroutine = StartCoroutine(Flying((transform.position - startTarget).normalized));
        _flying = true;

        Throwed?.Invoke();
    }

    private void StopFly()
    {
        StopCoroutine(_flyCoroutine);
        _flying = false;

        Invoke(nameof(TryReturnInHand), _returnDelay);

        Stoped?.Invoke();
    }

    private void TryReturnInHand()
    {
        if (Hand == null && _lastHand != null && _returnAfterStoped == true)
            _lastHand.TryTakeItem(this);
    }

    private void OnStuck()
    {
        StopFly();
        Stuck?.Invoke();
    }

    private IEnumerator Flying(Vector3 startDirection)
    {
        Vector3 direction = startDirection;
        float fallSpeed = 0;
        float flySpeed = _flySpeed;
        float ricochetesCount = 0;

        Rigidbody.isKinematic = true;
        _previousPosition = transform.position;

        transform.LookAt(transform.position - direction);

        while (true)
        {
            fallSpeed += _gravity * Time.deltaTime;
            flySpeed -= _airDrag * Time.deltaTime;
            flySpeed = Mathf.Clamp(flySpeed, 0, _flySpeed);

            Vector3 moveVector = Vector3.forward * flySpeed * Time.deltaTime + Vector3.down * fallSpeed * Time.deltaTime;
            transform.Translate(moveVector);

            TryCollide(ref ricochetesCount);

            _previousPosition = transform.position;

            yield return new WaitForEndOfFrame();
        }
    }

    private void TryCollide(ref float ricochetesCount)
    {
        float moveStepDistance = (_previousPosition - transform.position).magnitude;
        Ray ray = new Ray(_previousPosition, (transform.position - _previousPosition).normalized);
        if(Physics.Raycast(ray, out RaycastHit hit, moveStepDistance, AttackTargets))
        {
            if(hit.collider != null && hit.collider != _lastCollision && hit.collider && TryGetHealthFromCollider(hit.collider) != SelfHealth)
            {
                transform.position = hit.point;

                if (TryDamageCollider(hit.collider) == true)
                {
                    OnStuck();
                    Rigidbody.isKinematic = false;
                }
                else
                {
                    if (Vector3.Angle(hit.normal, -ray.direction) >= _ricocheteAngle && ricochetesCount < _maxRicochetesCount)
                    {
                        transform.LookAt(transform.position + Vector3.Reflect(ray.direction, hit.normal));
                        ricochetesCount++;

                        Ricochete?.Invoke();
                    }
                    else
                    {
                        transform.parent = hit.collider.transform;
                        OnStuck();
                    }
                }

                _lastCollision = hit.collider;
            }
        }
    }
}
