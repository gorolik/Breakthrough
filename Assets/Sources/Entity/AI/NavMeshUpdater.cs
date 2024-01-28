using UnityEngine;
using UnityEngine.AI;

public class NavMeshUpdater : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _navMeshSurface;


    private void Start()
    {
        RebuildMesh();
    }

    private void OnEnable()
    {
        Door.DoorLockUpdated += RebuildMesh;
    }

    private void OnDisable()
    {
        Door.DoorLockUpdated -= RebuildMesh;
    }

    public void RebuildMesh()
    {
        _navMeshSurface.BuildNavMesh();
    }
}
