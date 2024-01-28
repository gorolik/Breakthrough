public class UnlockedDoor : Door
{
    public override void Interact()
    {
        SwitchState();
    }

    protected override bool CanInteract()
    {
        return true;
    }
}
