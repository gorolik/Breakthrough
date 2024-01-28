using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivityChanger : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private PlayerLoock _playerLoock;

    private const float _defaultValue = 1f;
    private const string _sensitivityKey = "MouseSensitivity";


    private void Start()
    {
        _playerLoock = FindObjectOfType<PlayerLoock>();

        float value = Load();
        _slider.value = value;
        TryApplyMouseSensitivity(value);
    }

    private void OnEnable()
    {
        if (_slider != null)
            _slider.onValueChanged.AddListener(OnSensitivityValueChanged);
    }

    private void OnDisable()
    {
        if (_slider != null)
            _slider.onValueChanged.RemoveListener(OnSensitivityValueChanged);
    }

    public void OnSensitivityValueChanged(float value)
    {
        Save(value);
        TryApplyMouseSensitivity(value);
    }

    private void TryApplyMouseSensitivity(float value)
    {
        if(_playerLoock == null)
            return;

        _playerLoock.Sensitive = value;
    }

    private void Save(float value)
    {
        PlayerPrefs.SetFloat(_sensitivityKey, value);
    }

    private float Load()
    {
        if(PlayerPrefs.HasKey(_sensitivityKey) == false)
            PlayerPrefs.SetFloat(_sensitivityKey, _defaultValue);

        return PlayerPrefs.GetFloat(_sensitivityKey);
    }
}
