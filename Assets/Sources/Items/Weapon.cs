using UnityEngine;
using System;

public class Weapon : Item
{
    [SerializeField] private float _attackInterval;
    [SerializeField] private float _attackDistance;

    [SerializeField] protected LayerMask AttackTargets;

    protected TargetDestinator TargetDestinator;
    protected Health SelfHealth;

    private bool _readyToAttack = true;

    public float AttackDistance => _attackDistance;

    public Action Attacking;
    public Action ReadyToAttack;


    protected void OnEnable()
    {
        Taked += OnTaked;
    }

    protected void OnDisable()
    {
        Taked -= OnTaked;
    }

    public override void Use()
    {
        if (_readyToAttack == false)
            return;

        Attack();
    }

    protected override bool CanBeDroped()
    {
        return _readyToAttack;
    }

    protected bool TryDamageCollider(Collider collider)
    {
        Health health = TryGetHealthFromCollider(collider);
        if (health != null && health != SelfHealth)
        {
            health.Hit();
            return true;
        }

        return false;
    }

    protected Health TryGetHealthFromCollider(Collider collider)
    {
        Health health;
        if (collider.TryGetComponent(out health) || (collider.transform.parent != null && (health = collider.transform.parent.GetComponentInParent<Health>()) == true))
            return health;

        return null;
    }

    private void Attack()
    {
        if (TargetDestinator == null)
        {
            Debug.LogError("No Target Destinator on entity hand");
            return;
        }

        _readyToAttack = false;

        RaycastHit hit = TargetDestinator.GetTarget(_attackDistance, AttackTargets);

        if(hit.collider != null)
            TryDamageCollider(hit.collider);

        Invoke(nameof(RestoreReadyToAttack), _attackInterval);
        Attacking?.Invoke();
    }

    private void OnTaked()
    {
        Hand.TryGetComponent(out TargetDestinator);

        if ((SelfHealth = Hand.GetComponentInParent<Health>()) == false)
            Debug.LogWarning("SelfHealth on " + Hand.name + " not found");
    }

    private void RestoreReadyToAttack()
    {
        _readyToAttack = true;

        ReadyToAttack?.Invoke();
    }
}
