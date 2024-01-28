using static CanvasGroupHideShow;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private InputMap _inputMap;
    [SerializeField] private string _mainMenuScene = "MainMenu";

    private bool _isOpened;
    private CursorLocker _cursorLocker;


    private void Start()
    {
        _cursorLocker = FindObjectOfType<CursorLocker>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(_inputMap.OpenClosePauseMenu.Key) || Input.GetKeyDown(_inputMap.OpenClosePauseMenu.JoystickKey))
        {
            if(_isOpened == true)
                Close();
            else
                Open();
        }
    }

    public void Open()
    {
        if (_cursorLocker != null)
            _cursorLocker.SetCursorLock(false);

        ShowGroup(_canvasGroup);
        Time.timeScale = 0;

        _isOpened = true;
    }

    public void Close()
    {
        if (_cursorLocker != null)
            _cursorLocker.SetCursorLock(true);

        HideGroup(_canvasGroup);
        Time.timeScale = 1;

        _isOpened = false;
    }

    public void OnContinueButtonClicked()
    {
        Close();
    }

    public void OnExitButtonClicked()
    {
        Close();

        if (_cursorLocker != null)
            _cursorLocker.SetCursorLock(false);

        SceneManager.LoadScene(_mainMenuScene);
    }
}
