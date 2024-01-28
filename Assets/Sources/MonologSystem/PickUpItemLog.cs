using UnityEngine;

[RequireComponent(typeof(Item))]
public class PickUpItemLog : LogSender
{
    private Item _item;

    private bool _pickedOnce = false;


    private void OnEnable()
    {
        if (_item == null)
            _item = GetComponent<Item>();

        _item.Taked += OnItemPickedUp;
    }

    private void OnDisable()
    {
        if (_item != null)
            _item.Taked -= OnItemPickedUp;
    }

    private void OnItemPickedUp()
    {
        if (_pickedOnce == true)
            return;

        Send();

        _pickedOnce = true;
    }
}
