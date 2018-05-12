using Debugging;

namespace InputManager
{
    public class InputSystem
    {
        #region Variables
        private static InputMonitor inputMonitor;
        private static bool inputsEnabled = true;
        #endregion        

        #region Init Methods
        public static void InitInputMonitor()
        {
            if (inputMonitor == null)
            {
                Init();
            }
        }

        private static void Init()
        {
            inputsEnabled = true;
            inputMonitor = new InputMonitor();
            inputMonitor.Init();
            inputMonitor.InitHandlers(DispatchOnController, DispatchOnButton, DispatchOnStick, DispatchOnTrigger, DispatchOnStickAxis, DispatchOnTriggerAxis);
        }
        #endregion

        #region Class Methods
        public static void Update()
        {
            inputMonitor.Update();
        }

        public static void DisableInputs()
        {
            inputsEnabled = false;
        }

        public static void EnableInputs()
        {
            inputsEnabled = true;
        }

        public static bool AreInputsEnabled()
        {
            return inputsEnabled;
        }
        #endregion

        #region Delegates
        public delegate void ControllerHandler(int index, ControllerState controllerState);
        public delegate void ButtonHandler(int index, InputButton button, InputState state);
        public delegate void StickHandler(int index, InputStick stick, InputDirection direction, InputState state);
        public delegate void TriggerHandler(int index, InputTrigger trigger, InputState state);
        public delegate void StickAxisHandler(int index, InputStick stick, InputAxis axis, float value);
        public delegate void TriggerAxisHandler(int index, InputTrigger trigger, float value);
        #endregion

        #region private Events
        private static event ControllerHandler _OnController;
        private static event ButtonHandler _OnButton;
        private static event StickHandler _OnStick;
        private static event TriggerHandler _OnTrigger;
        private static event StickAxisHandler _OnStickAxis;
        private static event TriggerAxisHandler _OnTriggerAxis;
        #endregion

        #region public Events
        public static event ControllerHandler OnController
        {
            add { InitInputMonitor(); _OnController += value; }
            remove { _OnController -= value; }
        }
        public static event ButtonHandler OnButton
        {
            add { InitInputMonitor(); _OnButton += value; }
            remove { _OnButton -= value; }
        }
        public static event StickHandler OnStick
        {
            add { InitInputMonitor(); _OnStick += value; }
            remove { _OnStick -= value; }
        }
        public static event TriggerHandler OnTrigger
        {
            add { InitInputMonitor(); _OnTrigger += value; }
            remove { _OnTrigger -= value; }
        }
        public static event StickAxisHandler OnStickAxis
        {
            add { InitInputMonitor(); _OnStickAxis += value; }
            remove { _OnStickAxis -= value; }
        }
        public static event TriggerAxisHandler OnTriggerAxis
        {
            add { InitInputMonitor(); _OnTriggerAxis += value; }
            remove { _OnTriggerAxis -= value; }
        }
        #endregion

        #region Event Dispatches
        private static void DispatchOnController(int index, ControllerState controllerState)
        {
            if (_OnController != null)
            {
                _OnController(index, controllerState);
            }
            else
            {
                DebugTools.Log("No Listeners for DispatchOnController");
            }
        }
        private static void DispatchOnButton(int index, InputButton button, InputState state)
        {
            if (_OnButton != null)
            {
                _OnButton(index, button, state);
            }
            else
            {
                DebugTools.Log("No Listeners for DispatchOnButton");
            }
        }
        private static void DispatchOnStick(int index, InputStick stick, InputDirection direction, InputState state)
        {
            if (_OnStick != null)
            {
                _OnStick(index, stick, direction, state);
            }
            else
            {
                DebugTools.Log("No Listeners for DispatchOnStick");
            }
        }
        private static void DispatchOnTrigger(int index, InputTrigger trigger, InputState state)
        {
            if (_OnStick != null)
            {
                _OnTrigger(index, trigger, state);
            }
            else
            {
                DebugTools.Log("No Listeners for DispatchOnTrigger");
            }
        }
        private static void DispatchOnStickAxis(int index, InputStick stick, InputAxis axis, float value)
        {
            if (_OnStickAxis != null)
            {
                _OnStickAxis(index, stick, axis, value);
            }
            else
            {
                DebugTools.Log("No Listeners for DispatchOnStickAxis");
            }
        }
        private static void DispatchOnTriggerAxis(int index, InputTrigger trigger, float value)
        {
            if (_OnStickAxis != null)
            {
                _OnTriggerAxis(index, trigger, value);
            }
            else
            {
                DebugTools.Log("No Listeners for DispatchOnTriggerAxis");
            }
        }
        #endregion
    }
}

