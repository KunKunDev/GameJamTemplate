using UnityEngine;

namespace InputManager
{
    public class KeyboardInputMonitor
    {
        #region Events
        private InputSystem.ControllerHandler DispatchOnController;
        private InputSystem.ButtonHandler DispatchOnButton;
        private InputSystem.StickHandler DispatchOnStick;
        private InputSystem.TriggerHandler DispatchOnTrigger;

        private InputSystem.StickAxisHandler DispatchOnStickAxis;
        private InputSystem.TriggerAxisHandler DispatchOnTriggerAxis;
        #endregion

        #region Init Methods
        public void InitHandlers(InputSystem.ControllerHandler onController,
                                InputSystem.ButtonHandler onButton,
                                InputSystem.StickHandler onStick,
                                InputSystem.TriggerHandler onTrigger,
                                InputSystem.StickAxisHandler onStickAxis,
                                InputSystem.TriggerAxisHandler onTriggerAxis)
        {
            DispatchOnController = onController;
            DispatchOnButton = onButton;
            DispatchOnStick = onStick;
            DispatchOnTrigger = onTrigger;
            DispatchOnStickAxis = onStickAxis;
            DispatchOnTriggerAxis = onTriggerAxis;
        }
        #endregion

        #region Class Methods
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DispatchOnButton(0, InputButton.A, InputState.Down);
            }
        }
        #endregion
    }
}