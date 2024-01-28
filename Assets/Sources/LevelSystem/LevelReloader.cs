using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReloader : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private float _reloadDelay = 1;


    private void OnEnable()
    {
        _playerHealth.Die += OnPlayerDie;
    }

    private void OnDisable()
    {
        _playerHealth.Die -= OnPlayerDie;
    }

    private void OnPlayerDie()
    {
        Invoke(nameof(Reload), _reloadDelay);
    }

    private void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
