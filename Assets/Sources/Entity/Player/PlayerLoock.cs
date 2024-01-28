using UnityEngine;

public class PlayerLoock : MonoBehaviour
{
    [SerializeField] private InputMap _inputMap;
    [SerializeField] private CursorLocker _locker;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _minX = -360;
    [SerializeField] private float _maxX = 360;
    [SerializeField] private float _minY = -90;
    [SerializeField] private float _maxY = 90;

    public float Sensitive = 1.5f;
    public bool CanLook = true;

    private bool _cursorLocked;
    private float _mouseX;
    private float _mouseY;
    private Quaternion _bodyOriginalRot = Quaternion.identity;
    private Quaternion _cameraOriginalRot = Quaternion.identity;


    private void Awake()
    {
        _bodyOriginalRot = transform.localRotation;
        _cameraOriginalRot = _camera.localRotation;
    }

    private void OnEnable()
    {
        _locker.CursorStateChanched += OnCursorStateChanched;
    }

    private void OnDisable()
    {
        _locker.CursorStateChanched -= OnCursorStateChanched;
    }

    private void Update()
    {
        if(_cursorLocked == true && CanLook == true)
            Loock();
    }

    private void OnCursorStateChanched(bool value)
    {
        _cursorLocked = value;
    }

    private void Loock()
    {
        _mouseX += Input.GetAxis(_inputMap.GetUsingAxis(_inputMap.CameraLoockX)) * Sensitive;
        _mouseY += Input.GetAxis(_inputMap.GetUsingAxis(_inputMap.CameraLoockY)) * Sensitive;

        _mouseX = _mouseX % 360;
        _mouseY = _mouseY % 360;

        _mouseX = Mathf.Clamp(_mouseX, _minX, _maxX);
        _mouseY = Mathf.Clamp(_mouseY, _minY, _maxY);

        Quaternion xQuaternion = Quaternion.AngleAxis(_mouseX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(_mouseY, Vector3.left);

        transform.localRotation = _bodyOriginalRot * xQuaternion;
        _camera.transform.localRotation = _cameraOriginalRot * yQuaternion;
    }
}
