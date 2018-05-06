using Debugging;
using InputManager;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private void Awake()
    {
        InputSystem.OnButton += InputSystem_OnButton;
    }

    private void InputSystem_OnButton(int index, ButtonType button, State state)
    {
        DebugTools.Log(index + " / " + button + " / " + state);
    }
}
