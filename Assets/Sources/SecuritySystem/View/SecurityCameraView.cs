using UnityEngine;

[RequireComponent(typeof(SecurityCamera))]
public class SecurityCameraView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _sparks;
    [SerializeField] private AudioSource _disableSoundSource;
    [SerializeField] private Light _light;
    [SerializeField] private MeshRenderer _workIndicator;

    private SecurityCamera _securityCamera;


    private void Awake()
    {
        _securityCamera = GetComponent<SecurityCamera>();
    }

    private void OnEnable()
    {
        _securityCamera.Deactivated += OnDeactivated;
    }

    private void OnDisable()
    {
        _securityCamera.Deactivated -= OnDeactivated;
    }

    private void OnDeactivated()
    {
        _disableSoundSource.Play();
        _sparks.Play();
        _light.enabled = false;
        _workIndicator.material.DisableKeyword("_EMISSION");
    }
}
