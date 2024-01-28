using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _ObservePoint;
    [SerializeField] private Transform _ObserveTarget;

    public Transform ObservePoint => _ObservePoint;
    public Transform ObserveTarget => _ObserveTarget;
}
