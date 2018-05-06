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
            inputMonitor.InitButtons(DispatchOnButton);
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
        public delegate void ButtonHandler(int index, ButtonType button, State state);
        #endregion

        #region private Events
        private static event ButtonHandler _OnButton;
        #endregion

        #region public Events
        public static event ButtonHandler OnButton
        {
            add { InitInputMonitor(); _OnButton += value; }
            remove { _OnButton -= value; }
        }
        #endregion

        #region Event Dispatches
        private static void DispatchOnButton(int index, ButtonType button, State state)
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
        #endregion
    }
}

