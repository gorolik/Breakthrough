using UnityEngine;
using System;
using System.Reflection;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private InputMap _inputMap;
    [SerializeField] private Hand _hand;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _rayDistance;

    private Selectable _selected;
    private bool _isObjectSelected;

    public bool CanInteract = true;

    public Action<string> SelectableHit;
    public Action<string, string> InteractableHit;
    public Action<string, string> ItemHit;
    public Action NothingHit;


    private void Update()
    {
        if (CanInteract == false)
            return;

        Selectable selectable = TryGetSelectable();

        if (selectable == null || selectable.IsSelectable == false)
        {
            if (_selected != null || _isObjectSelected == true)
            {
                NothingHit?.Invoke();
                _isObjectSelected = false;
                _selected = null;
            }

            return;
        }
        else
        {
            KeyCode interactButton = _inputMap.GetUsingKey(_inputMap.Interact);

            if (selectable.TryGetComponent(out Interactable interactable))
            {
                if (_selected != selectable)
                    InteractableHit?.Invoke(interactable.Name, interactButton.ToString());

                if (Input.GetKeyDown(interactButton))
                    interactable.Interact(_hand.Item);
            }
            else if (selectable.TryGetComponent(out Item item))
            {
                if (_selected != selectable)
                    ItemHit?.Invoke(item.Name, interactButton.ToString());

                if (Input.GetKeyDown(interactButton))
                    _hand.TryTakeItem(item);
            }
            else
            {
                if (_selected != selectable)
                    SelectableHit?.Invoke(selectable.Name);
            }

            _isObjectSelected = true;
            _selected = selectable;
        }
    }

    private Selectable TryGetSelectable()
    {
        RaycastHit hit;
        if (Physics.Raycast(_playerCamera.ScreenPointToRay(Input.mousePosition), out hit, _rayDistance, _layerMask))
        {
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out Selectable selectable))
                    return selectable;
                else if (hit.collider.TryGetComponent(out SelectableChild child))
                    return child.Parent;
            }
        }

        return null;
    }
}
