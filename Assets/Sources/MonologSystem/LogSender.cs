using UnityEngine;

public abstract class LogSender : MonoBehaviour
{
    [SerializeField] private string _text = "Some Text";
    [SerializeField] private float _duration = 8;

    private MonologUIView _monologSystem;

    private void Start()
    {
        if ((_monologSystem = FindObjectOfType<MonologUIView>()) == false)
            Debug.LogError("Monolog System not found");
    }

    protected void Send()
    {
        if (_monologSystem == null)
            return;

        _monologSystem.Log(_text, _duration);
    }
}
