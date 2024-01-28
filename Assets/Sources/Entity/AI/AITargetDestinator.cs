using UnityEngine;

public class AITargetDestinator : TargetDestinator
{
    [SerializeField] private Enemy _aiController;
    [SerializeField] private Transform _loockPoint;

    public override RaycastHit GetTarget(float distance, LayerMask targets)
    {
        RaycastHit hit;
        Vector3 direction = _aiController.PlayerObservePoint.position - _loockPoint.position;

        Debug.DrawRay(_loockPoint.position, direction * distance, Color.red, 2);

        if (Physics.Raycast(_loockPoint.position, direction, out hit, distance, targets))
        {
            return hit;
        }
        else
        {
            RaycastHit emptyHit = new RaycastHit();
            emptyHit.point = _loockPoint.forward * distance;
            return emptyHit;
        }
    }
} 
