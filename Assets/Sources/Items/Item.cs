using UnityEngine;
using System;

public abstract class Item : Selectable
{
    [SerializeField] protected Rigidbody Rigidbody;

    private const int _freeLayer = 6;
    private const int _takedLayer = 8;

    protected Transform Hand;

    public Action Taked;
    public Action Released;


    public void Take(Transform hand)
    {
        Hand = hand;
        transform.parent = hand;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        Rigidbody.isKinematic = true;
        ChangeLayer(gameObject, _takedLayer);

        Taked?.Invoke();
    }

    public bool TryDrop()
    {
        if (CanBeDroped() == false)
            return false;

        Release();

        return true;
    }

    public virtual void Use()
    {
        return;
    }

    public virtual void AlternativeUse()
    {
        return;
    }

    protected virtual bool CanBeDroped()
    {
        return true;
    }

    protected void Release()
    {
        Hand = null;
        transform.parent = null;
        Rigidbody.isKinematic = false;
        ChangeLayer(gameObject, _freeLayer);

        Released?.Invoke();
    }

    private void ChangeLayer(GameObject gameObject, int layer)
    {
        gameObject.layer = layer;

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.layer = layer;

            Transform hasChild = child.GetComponentInChildren<Transform>();
            if (hasChild != null)
                ChangeLayer(child.gameObject, layer);
        }
    }
}
