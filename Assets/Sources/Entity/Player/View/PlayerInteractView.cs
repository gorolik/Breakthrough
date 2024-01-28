using UnityEngine;
using TMPro;

public class PlayerInteractView : MonoBehaviour
{
    [SerializeField] private PlayerInteract _playerInteract;
    [SerializeField] private TMP_Text _hint;


    private void Start()
    {
        OnNothingHit();
    }

    private void OnEnable()
    {
        _playerInteract.NothingHit += OnNothingHit;
        _playerInteract.SelectableHit += OnSelectableHit;
        _playerInteract.InteractableHit += OnInteractableHit;
        _playerInteract.ItemHit += OnItemHit;
    }

    private void OnDisable()
    {
        _playerInteract.NothingHit -= OnNothingHit;
        _playerInteract.SelectableHit -= OnSelectableHit;
        _playerInteract.InteractableHit -= OnInteractableHit;
        _playerInteract.ItemHit -= OnItemHit;
    }

    private void OnNothingHit()
    {
        _hint.enabled = false;
    }

    private void OnSelectableHit(string name)
    {
        _hint.enabled = true;
        _hint.text = name;
    }

    private void OnInteractableHit(string name, string interactKey)
    {
        _hint.enabled = true;
        _hint.text = name + $" ({interactKey})";
    }

    private void OnItemHit(string name, string takeKey)
    {
        _hint.enabled = true;
        _hint.text = name + $" ({takeKey})";
    }
}
