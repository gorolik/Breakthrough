using UnityEngine;
using System;
using System.Collections;

public class CursorLocker : MonoBehaviour
{
    [SerializeField] private InputMap _inputMap;
    [SerializeField] private bool _lockAtStart = true;
    [SerializeField] private bool _lockAtFocus = true;

    public Action<bool> CursorStateChanched;

    private bool _isLocked;


    private void Start()
    {
        StartCoroutine(SetLockCursorWithStart());
    }

    private void Update()
    {
        if(Input.GetKeyDown(_inputMap.ShowHideCursor.Key))
            SetCursorLock(!_isLocked);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus == true && _lockAtFocus == true)
            SetCursorLock(true);
    }

    public void SetCursorLock(bool isLocked)
    {
        _isLocked = isLocked;

        if (isLocked == true)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;

        CursorStateChanched?.Invoke(_isLocked);
    }

    private IEnumerator SetLockCursorWithStart()
    {
        yield return new WaitForSeconds(0.1f);

        SetCursorLock(_lockAtStart);
    }
}
