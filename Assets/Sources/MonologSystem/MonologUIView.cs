using static CanvasGroupHideShow;
using UnityEngine;
using System.Collections;
using TMPro;

public class MonologUIView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _monologGroup;
    [SerializeField] private AudioSource _logSound;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _printDelay = 0.02f;

    private float _hideTimer;
    private Coroutine _printer;


    private void Start()
    {
        HideGroup(_monologGroup);
    }

    private void Update()
    {
        if (_hideTimer > 0)
        {
            _hideTimer -= 1 * Time.deltaTime;

            if (_hideTimer <= 0)
            {
                _hideTimer = 0;
                HideLog();
            }
        }
    }

    public void Log(string text, float showTime)
    {
        ShowGroup(_monologGroup);

        if(_printer != null)
            StopCoroutine(_printer);
        _printer = StartCoroutine(PrintText(text));

        _hideTimer = showTime;

        _logSound.Play();
    }

    private void HideLog()
    {
        HideGroup(_monologGroup);
    }

    private IEnumerator PrintText(string text)
    {
        char[] chars = text.ToCharArray();
        _text.text = string.Empty;

        foreach (var symbol in chars)
        {
            _text.text = _text.text + symbol;
            
            yield return new WaitForSeconds(_printDelay);
        }

        _printer = null;
    }
}
