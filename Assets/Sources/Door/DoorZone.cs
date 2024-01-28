using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorZone : MonoBehaviour
{
    private Door _door;

    private void Start()
    {
        if ((_door = GetComponentInParent<Door>()) == false)
        {
            Debug.LogError("DoorZone cant find Door in parent");
        }
    }

    public Door GetDoor()
    {
        return _door;
    }
}
