using UnityEngine;

public class TriggerEnterLog : LogSender
{
    [SerializeField] private float _showDelay = 10;
    [SerializeField] private bool _onceTime = true;

    private float _delayTimer = 0;
    private bool _onceTimeUsed = false;

    private void Update()
    {
        if (_delayTimer > 0)
        {
            _delayTimer -= 1 * Time.deltaTime;

            if (_delayTimer <= 0)
            {
                _delayTimer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            OnPlayerEnterInTrigger();
        }
    }

    private void OnPlayerEnterInTrigger()
    {
        if (_delayTimer != 0 || (_onceTime == true && _onceTimeUsed == true))
            return;

        Send();

        _onceTimeUsed = true;
        _delayTimer = _showDelay;
    }
}
