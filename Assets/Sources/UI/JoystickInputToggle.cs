using UnityEngine;
using UnityEngine.UI;

public class JoystickInputToggle : MonoBehaviour
{
    [SerializeField] private InputMap _inputMap;
    [SerializeField] private Toggle _toggle;

    private const int _defaultValue = 0;
    private const string _joystickEnabledKey = "JoystickEnabled";


    private void Start()
    {
        int value = Load();

        if (value == 0)
        {
            _toggle.isOn = false;
            TryApply(false);
        }
        else
        {
            _toggle.isOn = true;
            TryApply(true);
        }
    }

    private void OnEnable()
    {
        if (_toggle != null)
            _toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        if (_toggle != null)
            _toggle.onValueChanged.RemoveListener(OnValueChanged);
    }

    public void OnValueChanged(bool value)
    {
        if(value == true)
            Save(1);
        else
            Save(0);

        TryApply(value);
    }

    private void TryApply(bool value)
    {
        if (_inputMap == null)
            return;

        _inputMap.EnableJoystickInput = value;
    }

    private void Save(int value)
    {
        PlayerPrefs.SetInt(_joystickEnabledKey, value);
    }

    private int Load()
    {
        if (PlayerPrefs.HasKey(_joystickEnabledKey) == false)
            PlayerPrefs.SetInt(_joystickEnabledKey, _defaultValue);

        return PlayerPrefs.GetInt(_joystickEnabledKey);
    }
}
