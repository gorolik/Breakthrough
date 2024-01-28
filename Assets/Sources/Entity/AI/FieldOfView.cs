using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float _angle;
    [SerializeField] private float _distance;
    [SerializeField] private float _rentgenDistance;
    [SerializeField] private Transform _loockPoint;
    [SerializeField] private LayerMask _layerMask;


    public bool IsTargetInView(Transform rayTarget, Transform objectTarget)
    {
        if (Vector3.Distance(_loockPoint.position, rayTarget.position) > _distance)
            return false;
        else if (Vector3.Distance(_loockPoint.position, rayTarget.position) < _rentgenDistance)
            return true;

        RaycastHit hit;
        Vector3 direction = rayTarget.position - _loockPoint.position;
        Ray ray = new Ray(_loockPoint.position, direction);
        float horizontalAngle = Mathf.Abs(Vector3.SignedAngle(_loockPoint.forward, direction, Vector3.forward));

        if (Physics.Raycast(ray, out hit, _distance, _layerMask))
        {
            if (horizontalAngle >= _angle / 2f)
                return false;
            else if (hit.collider.transform == objectTarget)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 left = _loockPoint.position + Quaternion.Euler(new Vector3(0, _angle / 2f, 0)) * _loockPoint.forward * _distance;
        Vector3 right = _loockPoint.position + Quaternion.Euler(-new Vector3(0, _angle / 2f, 0)) * _loockPoint.forward * _distance;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_loockPoint.position, left);
        Gizmos.DrawLine(_loockPoint.position, right);
        Gizmos.DrawLine(_loockPoint.position, _loockPoint.position + _loockPoint.forward * _distance);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_loockPoint.position, _rentgenDistance);
    }
}
