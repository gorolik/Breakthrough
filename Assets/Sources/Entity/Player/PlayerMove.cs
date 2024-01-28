using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private InputMap _inputMap;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _smoothMoveTime = 0.1f;
    [SerializeField] private float _gravity;

    public bool CanMoving = true;

    private CharacterController _characterController;
    private Vector3 _velocity;
    private float _verticalVelocity;
    private Vector3 _smoothV;


    public Vector3 Velocity => _characterController.velocity;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(CanMoving == true)
            Move();
    }

    private void ReadInput()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw(_inputMap.GetUsingAxis(_inputMap.MoveHorizontal)), Input.GetAxisRaw(_inputMap.GetUsingAxis(_inputMap.MoveVertical)));
        Vector3 inputDirection = new Vector3(input.x, 0, input.y).normalized;
        Vector3 worldInputDirection = transform.TransformDirection(inputDirection);
        Vector3 targetVelocity = worldInputDirection * _walkSpeed;

        _velocity = Vector3.SmoothDamp(_velocity, targetVelocity, ref _smoothV, _smoothMoveTime);
        _verticalVelocity -= _gravity * Time.deltaTime;
        _velocity = new Vector3(_velocity.x, _verticalVelocity, _velocity.z);
    }

    private void Move()
    {
        ReadInput();

        CollisionFlags flags = _characterController.Move(_velocity * Time.deltaTime);
        if (flags == CollisionFlags.Below)
        {
            _verticalVelocity = 0;
        }
    }
}
