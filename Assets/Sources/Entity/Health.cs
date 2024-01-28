using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public Action Die;


    public void Hit()
    {
        Die?.Invoke();
    }
}
