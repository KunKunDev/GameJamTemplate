using XInputDotNetPure;

namespace InputManager
{
    public class InputMonitor 
    {
        #region Settings from the InputManager ConfigFile
        private InputConfig inputConfig;
        #endregion

        #region Variables
        private int playerCount;
        private bool[] playerIndexSet;
        private GamePadState[] state;
        private GamePadState[] prevState;
        private PlayerIndex[] playerIndex;
        #endregion

        #region Events
        private InputSystem.ControllerHandler DispatchOnController;
        private InputSystem.ButtonHandler DispatchOnButton;
        private InputSystem.StickHandler DispatchOnStick;
        private InputSystem.TriggerHandler DispatchOnTrigger;

        private InputSystem.StickAxisHandler DispatchOnStickAxis;
        private InputSystem.TriggerAxisHandler DispatchOnTriggerAxis;
        #endregion

        #region Init Methods
        public void Init()
        {
            /// TODO: Init with config file
            playerCount = 4;

            playerIndexSet = new bool[playerCount];
            state = new GamePadState[playerCount];
            prevState = new GamePadState[playerCount];
            playerIndex = new PlayerIndex[playerCount];
        }

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

        #region Mono Methods
        public void Update()
        {
            if (!InputSystem.AreInputsEnabled())
            {
                return;
            }

            for (int i = 0; i < playerCount; ++i)
            {
                if (!playerIndexSet[i])
                {
                    PlayerIndex testPlayerIndex = (PlayerIndex)i;
                    GamePadState testState = GamePad.GetState(testPlayerIndex);
                    if (testState.IsConnected)
                    {
                        playerIndex[i] = testPlayerIndex;
                        playerIndexSet[i] = true;
                    }
                }
                else
                {
                    prevState[i] = state[i];
                    state[i] = GamePad.GetState(playerIndex[i]);

                    HandleControllers(i);
                    HandleButtons(i);
                    HandleSticks(i);
                    HandleTriggers(i);
                    HandleSticksAxis(i);
                    HandleTriggersAxis(i);
                }
            }
        }
        #endregion

        #region Class Methods
        private void HandleControllers(int i)
        {
            if (!prevState[i].IsConnected && state[i].IsConnected)
            {
                DispatchOnController(i, ControllerState.Connection);
            }
            else if (prevState[i].IsConnected && !state[i].IsConnected)
            {
                DispatchOnController(i, ControllerState.Disconnection);
                playerIndexSet[i] = false;
            }
        }

        private void HandleButtons(int i)
        {
            HandleButton(i, state[i].Buttons.A, prevState[i].Buttons.A, InputButton.A);
            HandleButton(i, state[i].Buttons.B, prevState[i].Buttons.B, InputButton.B);
            HandleButton(i, state[i].Buttons.X, prevState[i].Buttons.X, InputButton.X);
            HandleButton(i, state[i].Buttons.Y, prevState[i].Buttons.Y, InputButton.Y);
            HandleButton(i, state[i].Buttons.LeftShoulder, prevState[i].Buttons.LeftShoulder, InputButton.LeftShoulder);
            HandleButton(i, state[i].Buttons.RightShoulder, prevState[i].Buttons.RightShoulder, InputButton.RightShoulder);
            HandleButton(i, state[i].Buttons.LeftStick, prevState[i].Buttons.LeftStick, InputButton.LeftStick);
            HandleButton(i, state[i].Buttons.RightStick, prevState[i].Buttons.RightStick, InputButton.RightStick);
            HandleButton(i, state[i].Buttons.Start, prevState[i].Buttons.Start, InputButton.Start);
            HandleButton(i, state[i].Buttons.Back, prevState[i].Buttons.Back, InputButton.Back);
            HandleButton(i, state[i].Buttons.Guide, prevState[i].Buttons.Guide, InputButton.Guide);
            HandleButton(i, state[i].DPad.Up, state[i].DPad.Up, InputButton.UpArrow);
            HandleButton(i, state[i].DPad.Down, state[i].DPad.Down, InputButton.DownArrow);
            HandleButton(i, state[i].DPad.Left, state[i].DPad.Left, InputButton.LeftArrow);
            HandleButton(i, state[i].DPad.Right, state[i].DPad.Right, InputButton.RightArrow);
        }

        private void HandleButton(int i, ButtonState current, ButtonState prev, InputButton type)
        {
            if (current == ButtonState.Pressed && prev == ButtonState.Released)
            {
                DispatchOnButton(i, type, InputState.Down);
            }
            else if (current == ButtonState.Pressed)
            {
                DispatchOnButton(i, type, InputState.Pressed);
            }
            else if (current == ButtonState.Released && prevState[i].IsConnected && prev == ButtonState.Pressed)
            {
                DispatchOnButton(i, type, InputState.Released);
            }
        }

        private void HandleSticks(int i)
        {
            HandleStick(i, InputStick.LeftStick, state[i].ThumbSticks.Left, prevState[i].ThumbSticks.Left);
            HandleStick(i, InputStick.RightStick, state[i].ThumbSticks.Right, prevState[i].ThumbSticks.Right);
        }

        private void HandleStick(int i, InputStick inputStick, GamePadThumbSticks.StickValue current, GamePadThumbSticks.StickValue prev)
        {
            if (current.X > 0 && prev.X == 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Right, InputState.Down);
            }
            else if (current.X < 0 && prev.X == 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Left, InputState.Down);
            }
            else if (current.X > 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Right, InputState.Pressed);
            }
            else if (current.X < 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Left, InputState.Pressed);
            }
            else if (current.X == 0 && prevState[i].IsConnected && prev.X > 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Right, InputState.Released);
            }
            else if (current.X == 0 && prevState[i].IsConnected && prev.X < 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Left, InputState.Released);
            }

            if (current.Y > 0 && prev.Y == 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Up, InputState.Down);
            }
            else if (current.Y < 0 && prev.Y == 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Down, InputState.Down);
            }
            else if (current.Y > 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Up, InputState.Pressed);
            }
            else if (current.Y < 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Down, InputState.Pressed);
            }
            else if (current.Y == 0 && prevState[i].IsConnected && prev.Y > 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Up, InputState.Released);
            }
            else if (current.Y == 0 && prevState[i].IsConnected && prev.Y < 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Down, InputState.Released);
            }
        }

        private void HandleTriggers(int i)
        {
            HandleTrigger(i, InputTrigger.Left, state[i].Triggers.Left, prevState[i].Triggers.Left);
            HandleTrigger(i, InputTrigger.Right, state[i].Triggers.Right, prevState[i].Triggers.Right);
        }

        private void HandleTrigger(int i, InputTrigger type, float trigger, float prevTrigger)
        {
            if (trigger > 0 && prevTrigger == 0)
            {
                DispatchOnTrigger(i, type, InputState.Down);
            }
            else if (trigger > 0)
            {
                DispatchOnTrigger(i, type, InputState.Pressed);
            }
            else if (trigger == 0 && prevState[i].IsConnected && prevTrigger > 0)
            {
                DispatchOnTrigger(i, type, InputState.Released);
            }
        }

        private void HandleSticksAxis(int i)
        {
            HandleStickAxis(i, InputStick.LeftStick, state[i].ThumbSticks.Left);
            HandleStickAxis(i, InputStick.RightStick, state[i].ThumbSticks.Right);
        }

        private void HandleStickAxis(int i, InputStick stick, GamePadThumbSticks.StickValue current)
        {
            DispatchOnStickAxis(i, stick, InputAxis.X, current.X);
            DispatchOnStickAxis(i, stick, InputAxis.Y, current.Y);
        }

        private void HandleTriggersAxis(int i)
        {
            DispatchOnTriggerAxis(i, InputTrigger.Left, state[i].Triggers.Left);
            DispatchOnTriggerAxis(i, InputTrigger.Right, state[i].Triggers.Right);
        }
        #endregion
    }

}
