using UnityEngine;

public class AutoDoorInteract : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DoorZone doorZone))
        {
            doorZone.GetDoor().TryOpen();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out DoorZone doorZone))
        {
            doorZone.GetDoor().TryClose();
        }
    }
}
