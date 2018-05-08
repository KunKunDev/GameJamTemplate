using Debugging;
using InputManager;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private void Awake()
    {
        InputSystem.OnButton += InputSystem_OnButton;
        InputSystem.OnStick += InputSystem_OnStick;
        InputSystem.OnTrigger += InputSystem_OnTrigger;
    }

    private void InputSystem_OnButton(int index, InputButton button, State state)
    {
        DebugTools.Log(index + " / " + button + " / " + state);
    }

    private void InputSystem_OnStick(int index, InputStick stick, InputDirection direction, State state)
    {
        DebugTools.Log(index + " / " + stick + " / " + direction + " / " + state);
    }

    private void InputSystem_OnTrigger(int index, Trigger trigger, State state)
    {
        DebugTools.Log(index + " / " + trigger + " / " + state);
    }
}
