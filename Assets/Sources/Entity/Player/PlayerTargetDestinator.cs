using UnityEngine;

public class PlayerTargetDestinator : TargetDestinator
{
    [SerializeField] private Camera _camera;

    public override RaycastHit GetTarget(float distance, LayerMask targets)
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, distance, targets))
        {
            return hit;
        }
        else
        {
            RaycastHit emptyHit = new RaycastHit();
            emptyHit.point = _camera.transform.forward * distance;
            return emptyHit;
        }
    }
}
