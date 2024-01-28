using UnityEngine;

public class Document : Interactable
{
    [SerializeField] private int _id;


    public override void Interact()
    {
        Documents.TryCollectDocument(_id);
        Destroy(gameObject);
    }
}
