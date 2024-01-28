using UnityEngine;

public class ItemView : MonoBehaviour
{
    [SerializeField] protected Item Item;
    [SerializeField] protected AudioSource AudioSource;
    [SerializeField] protected Animator Animator;

    [SerializeField] private AudioClip _takeSound;


    protected void OnEnable()
    {
        Item.Taked += OnItemTaked;
    }

    protected void OnDisable()
    {
        Item.Taked -= OnItemTaked;
    }

    private void OnItemTaked()
    {
        Animator.SetTrigger(ItemAnimatorFields.PuckupTrigger);

        if (_takeSound != null)
            AudioSource.PlayOneShot(_takeSound);
    }
}
