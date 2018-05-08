using Debugging;
using UnityEngine;

namespace InputManager
{
    public class InputSystem
    {
        #region Variables
        private static GameObject monitorParent;
        private static InputMonitor inputMonitor;
        private static bool inputsEnabled = true;
        #endregion

        #region Const variables
        private const string INPUT_MONITOR_NAME = "InputSystem";
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
            monitorParent = new GameObject(INPUT_MONITOR_NAME);
            inputMonitor = monitorParent.AddComponent<InputMonitor>();

            inputMonitor.Init();
            inputMonitor.InitButtons(DispatchOnButton, DispatchOnStick, DispatchOnTrigger);
        }
        #endregion

        #region Class Methods
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
        public delegate void ButtonHandler(int index, InputButton button, State state);
        public delegate void StickHandler(int index, InputStick stick, InputDirection direction, State state);
        public delegate void TriggerHandler(int index, Trigger trigger, State state);
        #endregion

        #region private Events
        private static event ButtonHandler _OnButton;
        private static event StickHandler _OnStick;
        private static event TriggerHandler _OnTrigger;
        #endregion

        #region public Events
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
        #endregion

        #region Event Dispatches
        private static void DispatchOnButton(int index, InputButton button, State state)
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
        private static void DispatchOnStick(int index, InputStick stick, InputDirection direction, State state)
        {
            if (_OnStick != null)
            {
                _OnStick(index, stick, direction, state);
            }
            else
            {
                DebugTools.Log("No Listeners for DispatchOnButton");
            }
        }
        private static void DispatchOnTrigger(int index, Trigger trigger, State state)
        {
            if (_OnStick != null)
            {
                _OnTrigger(index, trigger, state);
            }
            else
            {
                DebugTools.Log("No Listeners for DispatchOnButton");
            }
        }
        #endregion
    }
}

