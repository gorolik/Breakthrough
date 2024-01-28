using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private PlayerLoock _playerLoock;
    [SerializeField] private PlayerHand _playerHand;
    [SerializeField] private PlayerInteract _playerInteract;


    private void OnEnable()
    {
        _playerHealth.Die += OnPlayerDie;
    }

    private void OnDisable()
    {
        _playerHealth.Die -= OnPlayerDie;
    }

    public void Enable()
    {
        _playerMove.CanMoving = true;
        _playerLoock.CanLook = true;
        _playerHand.CanInteractWithItem = true;
        _playerInteract.CanInteract = true;
    }

    public void Disable()
    {
        _playerMove.CanMoving = false;
        _playerLoock.CanLook = false;
        _playerHand.CanInteractWithItem = false;
        _playerInteract.CanInteract = false;
    }

    private void OnPlayerDie()
    {
        Disable();
    }
}
