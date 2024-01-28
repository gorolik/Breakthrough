using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private Item _item;

    public Item Item => _item;


    private void Start()
    {
        if (_item != null)
            TakeItem(_item);
    }

    public void TryTakeItem(Item item)
    {
        if (TryDropItem() == false)
            return;

        TakeItem(item);
    }

    public bool TryDropItem()
    {
        if (_item == null)
            return true;

        if (_item.TryDrop() == true)
        {
            ReleaseItem();
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void TryUseItem()
    {
        if (_item == null)
            return;

        _item.Use();
    }

    protected void TryAlternativeUseItem()
    {
        if (_item == null)
            return;

        _item.AlternativeUse();
    }

    private void TakeItem(Item item)
    {
        _item = item;
        item.Released += OnItemReleased;
        item.Take(transform);
    }

    private void ReleaseItem()
    {
        if (_item == null)
            return;

        _item.Released -= OnItemReleased;
        _item = null;
    }

    private void OnItemReleased()
    {
        ReleaseItem();
    }
}
