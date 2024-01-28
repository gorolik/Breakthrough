using UnityEngine;

public class Locker : Interactable
{
    [SerializeField] private LockedDoor _lockedDoor;

    public override bool Interact(Item item)
    {
        return _lockedDoor.Interact(item);
    }

    public void Unlock()
    {
        Destroy(gameObject);
    }
}
