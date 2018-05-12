using InputManager;

public class PlayerInput
{
    public PlayerInput()
    {
        RegisterHandlers();
    }

    ~PlayerInput()
    {
        UnregisterHandlers();
    }

    private void RegisterHandlers()
    {
        InputSystem.OnController += InputSystem_OnController;
        InputSystem.OnButton += InputSystem_OnButton;
        InputSystem.OnStick += InputSystem_OnStick;
        InputSystem.OnTrigger += InputSystem_OnTrigger;
        InputSystem.OnStickAxis += InputSystem_OnStickAxis;
        InputSystem.OnTriggerAxis += InputSystem_OnTriggerAxis;
    }

    private void UnregisterHandlers()
    {
        InputSystem.OnController -= InputSystem_OnController;
        InputSystem.OnButton -= InputSystem_OnButton;
        InputSystem.OnStick -= InputSystem_OnStick;
        InputSystem.OnTrigger -= InputSystem_OnTrigger;
        InputSystem.OnStickAxis -= InputSystem_OnStickAxis;
        InputSystem.OnTriggerAxis -= InputSystem_OnTriggerAxis;
    }

    private void InputSystem_OnTriggerAxis(int index, InputTrigger trigger, float value)
    {

    }

    private void InputSystem_OnStickAxis(int index, InputStick stick, InputAxis axis, float value)
    {

    }

    private void InputSystem_OnController(int index, ControllerState controllerState)
    {

    }

    private void InputSystem_OnButton(int index, InputButton button, InputState state)
    {

    }

    private void InputSystem_OnStick(int index, InputStick stick, InputDirection direction, InputState state)
    {

    }

    private void InputSystem_OnTrigger(int index, InputTrigger trigger, InputState state)
    {

    }
}
