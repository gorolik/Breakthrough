using UnityEngine;

public class SelectableChild : MonoBehaviour
{
    [SerializeField] private Selectable _parent;

    public Selectable Parent => _parent;
}
