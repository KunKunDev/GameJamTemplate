using Debugging;
using InputManager;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private void Awake()
    {
        InputSystem.OnController += InputSystem_OnController;
        InputSystem.OnButton += InputSystem_OnButton;
        InputSystem.OnStick += InputSystem_OnStick;
        InputSystem.OnTrigger += InputSystem_OnTrigger;
    }

    private void InputSystem_OnController(int index, ControllerState controllerState)
    {
        DebugTools.Log(index + " / " + controllerState);
    }

    private void InputSystem_OnButton(int index, InputButton button, InputState state)
    {
        DebugTools.Log(index + " / " + button + " / " + state);
    }

    private void InputSystem_OnStick(int index, InputStick stick, InputDirection direction, InputState state)
    {
        DebugTools.Log(index + " / " + stick + " / " + direction + " / " + state);
    }

    private void InputSystem_OnTrigger(int index, InputTrigger trigger, InputState state)
    {
        DebugTools.Log(index + " / " + trigger + " / " + state);
    }
}
