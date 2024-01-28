using UnityEngine;

[RequireComponent(typeof(SecurityBlock))]
public class SecurityBlockView : MonoBehaviour
{
    [SerializeField] private GameObject _normalWires;
    [SerializeField] private GameObject _brokenWires;
    [SerializeField] private MeshRenderer _workIndicator;
    [SerializeField] private AudioSource _brokeSystemSource;
    [SerializeField] private AudioSource _sirenSource;
    [SerializeField] private ParticleSystem _sparks;

    private SecurityBlock _securityBlock;


    private void Awake()
    {
        _securityBlock = GetComponent<SecurityBlock>();
    }

    private void OnEnable()
    {
        _securityBlock.Deactivated += OnDeactivated;
        _securityBlock.PlayerDetecteded += OnPlayerDetected;
    }

    private void OnDisable()
    {
        _securityBlock.Deactivated -= OnDeactivated;
        _securityBlock.PlayerDetecteded -= OnPlayerDetected;
    }

    private void OnDeactivated()
    {
        _workIndicator.material.DisableKeyword("_EMISSION");
        _brokeSystemSource.Play();
        _sparks.Play();

        _normalWires.SetActive(false);
        _brokenWires.SetActive(true);
    }

    private void OnPlayerDetected()
    {
        if(_sirenSource.isPlaying == false)
            _sirenSource.Play();
    }
}
