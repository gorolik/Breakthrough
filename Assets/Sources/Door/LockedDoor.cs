using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Door
{
    [SerializeField] private Locker _locker;

    private bool _locked = true;


    public override bool Interact(Item item)
    {
        if(_locked == false)
        {
            SwitchState();
            return false;
        }
        else if(GetDoorOpenerRelative() == DoorOpenerRelative.BackDoor)
        {
            return false;
        }
        else
        {
            if(item is Knife)
            {
                _locker.Unlock();
                _locked = false;
                SwitchState();
                LockUpdate(false);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    protected override bool CanInteract()
    {
        if(_locked == true)
            return false;
        else
            return true;
    }
}
