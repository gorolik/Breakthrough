using UnityEngine;

public class WeaponView : ItemView
{
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private TrailRenderer _attackTrail;

    private Weapon _weapon;


    protected void Awake()
    {
        _weapon = Item as Weapon;
    }

    protected new void OnEnable()
    {
        base.OnEnable();

        _weapon.Attacking += OnAttack;
        _weapon.ReadyToAttack += OnReadyToAttack;
    }

    protected new void OnDisable()
    {
        base.OnDisable();

        _weapon.Attacking -= OnAttack;
        _weapon.ReadyToAttack -= OnReadyToAttack;
    }

    private void OnAttack()
    {
        Animator.SetTrigger(WeaponAnimatorFields.AttackTrigger);
        AudioSource.PlayOneShot(_attackSound);
        _attackTrail.emitting = true;
    }

    private void OnReadyToAttack()
    {
        _attackTrail.emitting = false;
    }
}
