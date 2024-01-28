using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private NextLevelLoader _nextLevelLoader;
    [SerializeField] private float _observeDistance = 1;

    private Transform _player;
    private bool _reached;


    private void Start()
    {
        _player = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        if (_player != null)
        {
            if(Vector3.Distance(transform.position, _player.position) <= _observeDistance && _reached == false)
            {
                _reached = true;
                _nextLevelLoader.LoadNextLevel();
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _observeDistance);
    }
}
