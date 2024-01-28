using UnityEngine;

public class Safe : Interactable
{
    [SerializeField] private Animator _animator;


    public override bool Interact(Item item)
    {
        if(item is Knife)
        {
            _animator.SetTrigger(SafeAnimatorFields.OpenTrigger);
            SetInteractable(false);
            return true;
        }
        else
        {
            return false;
        }
    }
}
