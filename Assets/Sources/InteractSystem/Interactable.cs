public abstract class Interactable : Selectable
{
    private bool _isInteractable = true;

    public bool IsInteractable => _isInteractable;


    protected void SetInteractable(bool value)
    {
        _isSelectable = value;
        _isInteractable = value;
    }

    public virtual void Interact()
    {

    }    

    public virtual bool Interact(Item item)
    {
        Interact();
        return false;
    }
}
