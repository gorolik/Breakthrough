using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private AudioSource _detectAudioSource;
    [SerializeField] private AudioClip _playerDetectSound;


    private void OnEnable()
    {
        _enemy.PlayerDetected += OnPlayerDetected;
    }

    private void OnDisable()
    {
        _enemy.PlayerDetected -= OnPlayerDetected;
    }

    private void OnPlayerDetected()
    {
        if(_detectAudioSource.isPlaying == false)   
            _detectAudioSource.PlayOneShot(_playerDetectSound);
    }
}
