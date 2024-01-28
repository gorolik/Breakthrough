using UnityEngine;

public class Selectable : MonoBehaviour
{
    [SerializeField] private string _name;

    protected bool _isSelectable = true;

    public string Name => _name;
    public bool IsSelectable => _isSelectable;
}
