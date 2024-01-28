using UnityEngine;

public class ThrowableView : WeaponView
{
    [SerializeField] private Transform _mesh;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private AudioClip _throwSound;
    [SerializeField] private AudioClip _stuckSound;
    [SerializeField] private AudioClip _ricocheteSound;
    [SerializeField] private TrailRenderer _flyTrail;

    private Throwable _throwable;
    private bool _flying;


    protected new void Awake()
    {
        base.Awake();

        _throwable = Item as Throwable;
    }

    protected void Update()
    {
        if (_flying == true) 
        {
            _mesh.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    protected new void OnEnable()
    {
        base.OnEnable();

        _throwable.Throwed += OnThrowed;
        _throwable.Stoped += OnStoped;
        _throwable.Stuck += OnStuck;
        _throwable.Taked += OnTaked;
        _throwable.Ricochete += OnRicochete;
    }

    protected new void OnDisable()
    {
        base.OnDisable();

        _throwable.Throwed -= OnThrowed;
        _throwable.Stoped -= OnStoped;
        _throwable.Stuck -= OnStuck;
        _throwable.Taked -= OnTaked;
        _throwable.Ricochete -= OnRicochete;
    }

    private void OnThrowed()
    {
        _mesh.localRotation = Quaternion.Euler(0, 0, -90);
        _flying = true;
        AudioSource.PlayOneShot(_throwSound);
        _flyTrail.emitting = true;
    }

    private void OnStoped()
    {
        _mesh.localRotation = Quaternion.Euler(0, 0, -90);
        _flying = false;
        _flyTrail.emitting = false;
    }

    private void OnStuck()
    {
        AudioSource.PlayOneShot(_stuckSound);
    }

    private void OnTaked()
    {
        _mesh.localRotation = Quaternion.identity;
    }

    private void OnRicochete()
    {
        AudioSource.PlayOneShot(_ricocheteSound);
    }
}
