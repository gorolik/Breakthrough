using UnityEngine;

public abstract class TargetDestinator : MonoBehaviour
{
    public abstract RaycastHit GetTarget(float distance, LayerMask targets);
}
