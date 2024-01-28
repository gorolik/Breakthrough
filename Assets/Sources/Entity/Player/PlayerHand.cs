using UnityEngine;

public class PlayerHand : Hand
{
    [SerializeField] private InputMap _inputMap;

    public bool CanInteractWithItem = true;


    private void Update()
    {
        if (CanInteractWithItem == false || Time.deltaTime == 0)
            return;

        if (Input.GetKeyDown(_inputMap.GetUsingKey(_inputMap.UseItem)))
            TryUseItem();
        else if (Input.GetKeyDown(_inputMap.GetUsingKey(_inputMap.AlternativeUseItem)))
            TryAlternativeUseItem();
        else if (Input.GetKeyDown(_inputMap.GetUsingKey(_inputMap.DropItem)))
            TryDropItem();
    }
}
