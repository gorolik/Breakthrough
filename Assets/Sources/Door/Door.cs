using static DoorAnimatorFields;
using System.Collections.Generic;
using UnityEngine;

public abstract class Door : Interactable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private DoorState _startState;
    [SerializeField] private bool _onceInteract = false;
    [SerializeField] private GameObject _barrier;

    public delegate void DoorLockUpdate();
    public static DoorLockUpdate DoorLockUpdated;

    public enum DoorState { Closed, OpenedInward, OpenedBackward }
    private DoorState _state;
    public enum DoorOpenerRelative { FrontDoor, BackDoor }

    private DoorOpener[] _doorOpeners;
    private bool _onceInteracted = false;

    protected void Start()
    {
        PlayAnimation(_startState);
        _state = _startState;

        _doorOpeners = FindObjectsOfType<DoorOpener>();
    }

    public void TryOpen()
    {
        if (CanInteract() == false)
            return;

        Open();
    }

    public void TryClose()
    {
        if (CanInteract() == false)
            return;

        Close();
    }

    protected void Open()
    {
        Open(GetRelativeDoorState());
    }

    protected void Close()
    {
        if (_state == DoorState.Closed)
            return;

        PlayAnimation(DoorState.Closed);
        _state = DoorState.Closed;
    }

    protected void SwitchState()
    {
        if(_state == DoorState.Closed)
            Open();
        else
            Close();
    }

    protected void LockUpdate(bool isLocked)
    {
        _barrier.SetActive(isLocked);
        DoorLockUpdated?.Invoke();
    }

    protected DoorOpenerRelative GetDoorOpenerRelative()
    {
        Transform closeDoorOpener = null;
        UpdateDoorOpeners();

        if (_doorOpeners == null || _doorOpeners.Length == 0)
        {
            return DoorOpenerRelative.FrontDoor;
        }

        foreach (var doorOpener in _doorOpeners)
        {
            if (closeDoorOpener == null)
            {
                closeDoorOpener = doorOpener.transform;
                continue;
            }

            if (Vector3.Distance(doorOpener.transform.position, transform.position) < Vector3.Distance(closeDoorOpener.position, transform.position))
            {
                closeDoorOpener = doorOpener.transform;
            }
        }

        Vector3 backwardPoint = transform.position - transform.forward;
        Vector3 inwardPoint = transform.position + transform.forward;

        if (Vector3.Distance(closeDoorOpener.position, backwardPoint) < Vector3.Distance(closeDoorOpener.position, inwardPoint))
        {
            return DoorOpenerRelative.FrontDoor;
        }
        else
        {
            return DoorOpenerRelative.BackDoor;
        }
    }

    protected abstract bool CanInteract();

    private void Open(DoorState doorState)
    {
        if (doorState == DoorState.Closed || (_onceInteract == true && _onceInteracted == true))
            return;

        _onceInteracted = true;
        PlayAnimation(doorState);
        _state = doorState;
    }

    private DoorState GetRelativeDoorState()
    {
        DoorOpenerRelative doorOpenerRelative = GetDoorOpenerRelative();

        if (doorOpenerRelative == DoorOpenerRelative.FrontDoor)
            return DoorState.OpenedInward;
        else
            return DoorState.OpenedBackward;
    }

    private void PlayAnimation(DoorState state)
    {
        if (state == DoorState.Closed)
        {
            _animator.SetBool(ClosedBool, true);
            _animator.SetBool(OpenedInwardBool, false);
            _animator.SetBool(OpenedBackwardBool, false);
        }
        else if(state == DoorState.OpenedInward)
        {
            _animator.SetBool(ClosedBool, false);
            _animator.SetBool(OpenedInwardBool, true);
            _animator.SetBool(OpenedBackwardBool, false);
        }
        else if (state == DoorState.OpenedBackward)
        {
            _animator.SetBool(ClosedBool, false);
            _animator.SetBool(OpenedInwardBool, false);
            _animator.SetBool(OpenedBackwardBool, true);
        }
    }

    private void UpdateDoorOpeners()
    {
        List<DoorOpener> actualDoorOpeners = new List<DoorOpener>();

        foreach (var opener in _doorOpeners)
        {
            if(opener != null)
                actualDoorOpeners.Add(opener);
        }

        _doorOpeners = actualDoorOpeners.ToArray();
    }
}
