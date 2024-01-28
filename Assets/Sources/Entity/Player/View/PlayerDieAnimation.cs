using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerDieAnimation : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Volume _volume;
    [SerializeField] private Color _vignetteColor = Color.red;
    [SerializeField] private float _vignetteIntensity = 0.45f;
    [SerializeField] private Vector3 _cameraForce;
    [SerializeField] private Vector3 _cameraTorque;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _player;
    [SerializeField] private Animator _cameraAnimator;

    private Vignette _vignette;


    private void Start()
    {
        _volume.profile.TryGet(out _vignette);
    }

    private void OnEnable()
    {
        _health.Die += OnDie;
    }

    private void OnDisable()
    {
        _health.Die -= OnDie;
    }

    private void OnDie()
    {
        _cameraAnimator.enabled = false;
        _camera.transform.parent = null;

        Rigidbody rigidbody = _camera.GetComponent<Rigidbody>();
        Collider collider = _camera.GetComponent<Collider>();

        rigidbody.isKinematic = false;
        rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        collider.enabled = true;

        rigidbody.AddForce(GetRandomVector(_cameraForce));
        rigidbody.AddTorque(GetRandomVector(_cameraTorque));

        if (_vignette != null)
        {
            _vignette.color.value = _vignetteColor;
            _vignette.intensity.value = _vignetteIntensity;
        }

        StartCoroutine(PlayerDestroy());
    }

    private IEnumerator PlayerDestroy()
    {
        yield return new WaitForEndOfFrame();

        Destroy(_player);
    }

    private Vector3 GetRandomVector(Vector3 refVector)
    {
        return new Vector3(Random.Range(-refVector.x, refVector.x), Random.Range(-refVector.y, refVector.y), Random.Range(-refVector.z, refVector.z));
    }
}
