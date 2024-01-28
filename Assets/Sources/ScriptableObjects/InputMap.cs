using System;
using UnityEngine;

[CreateAssetMenu(fileName = "KeyMap", menuName = "Scriptable Objects/KeyMap", order = 51)]
public class InputMap : ScriptableObject
{
    public bool EnableJoystickInput = false;

    [Header("Axis")]
    public Axis MoveHorizontal = new Axis("Horizontal", "Horizontal");
    public Axis MoveVertical = new Axis("Vertical", "Vertical");
    public Axis CameraLoockX = new Axis("Mouse X", "");
    public Axis CameraLoockY = new Axis("Mouse Y", "");
    [Header("Keys")]
    public ActionKey UseItem = new ActionKey(KeyCode.Mouse0, KeyCode.None);
    public ActionKey AlternativeUseItem = new ActionKey(KeyCode.Mouse1, KeyCode.None);
    public ActionKey Interact = new ActionKey(KeyCode.E, KeyCode.None);
    public ActionKey DropItem = new ActionKey(KeyCode.G, KeyCode.None);
    public ActionKey ShowHideCursor = new ActionKey(KeyCode.Tab, KeyCode.None);
    public ActionKey OpenClosePauseMenu = new ActionKey(KeyCode.Escape, KeyCode.None);


    public KeyCode GetUsingKey(ActionKey actionKey)
    {
        if (EnableJoystickInput == true)
            return actionKey.JoystickKey;
        else
            return actionKey.Key;
    }

    public string GetUsingAxis(Axis axis)
    {
        if (EnableJoystickInput == true)
            return axis.JoystickAxisName;
        else
            return axis.AxisName;
    }

    [Serializable]
    public struct ActionKey
    {
        public KeyCode Key;
        public KeyCode JoystickKey;

        public ActionKey(KeyCode key, KeyCode joystickKey)
        {
            Key = key;
            JoystickKey = joystickKey;
        }
    }

    [Serializable]
    public struct Axis
    {
        public string AxisName;
        public string JoystickAxisName;

        public Axis(string axisName, string joystickAxisName)
        {
            AxisName = axisName;
            JoystickAxisName = joystickAxisName;
        }
    }
}
