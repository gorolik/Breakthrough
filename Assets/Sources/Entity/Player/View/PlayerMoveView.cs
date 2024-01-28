using static CameraAnimatorFields;
using UnityEngine;

public class PlayerMoveView : MonoBehaviour
{
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private float _playerSpeedAnimationFactor = 1;
    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private AudioSource _footStepsSource;
    [SerializeField] private AudioClip[] _footSteps;

    private float _minPlayerVelocity = 0.1f;


    private void Update()
    {
        if(_playerMove == null)
            return;

        if (_playerMove.Velocity.magnitude >= _minPlayerVelocity)
        {
            _cameraAnimator.SetBool(IsMovingBool, true);
            _cameraAnimator.SetFloat(MovingSpeedFloat, _playerMove.Velocity.magnitude * _playerSpeedAnimationFactor);
        }
        else
        {
            _cameraAnimator.SetBool(IsMovingBool, false);
        }
    }

    public void Step()
    {
        if (_footSteps.Length <= 0)
            return;

        _footStepsSource.PlayOneShot(_footSteps[Random.Range(0, _footSteps.Length)]);
    }
}
