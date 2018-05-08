using System;
using Debugging;
using UnityEngine;
using XInputDotNetPure;

namespace InputManager
{
    public class InputMonitor : MonoBehaviour
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
        private InputSystem.ButtonHandler DispatchOnButton;
        private InputSystem.StickHandler DispatchOnStick;
        private InputSystem.TriggerHandler DispatchOnTrigger;
        #endregion

        #region Init Methods
        public void Init()
        {
            //init with config file
            playerCount = 4;

            playerIndexSet = new bool[playerCount];
            state = new GamePadState[playerCount];
            prevState = new GamePadState[playerCount];
            playerIndex = new PlayerIndex[playerCount];
        }

        public void InitButtons(InputSystem.ButtonHandler onButton, InputSystem.StickHandler onStick, InputSystem.TriggerHandler onTrigger)
        {
            DispatchOnButton = onButton;
            DispatchOnStick = onStick;
            DispatchOnTrigger = onTrigger;
        }
        #endregion

        #region Mono Methods
        private void Update()
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
                        DebugTools.Log("GamePad found " + testPlayerIndex);
                        playerIndex[i] = testPlayerIndex;
                        playerIndexSet[i] = true;
                    }
                }
                else
                {
                    prevState[i] = state[i];
                    state[i] = GamePad.GetState(playerIndex[i]);

                    HandleButtons(i);
                    HandleSticks(i);
                    HandleTriggers(i);
                }
            }
        }
        #endregion

        #region Class Methods
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
                DispatchOnButton(i, type, State.Down);
            }
            else if (current == ButtonState.Pressed)
            {
                DispatchOnButton(i, type, State.Pressed);
            }
            else if (current == ButtonState.Released && prevState[i].IsConnected && prev == ButtonState.Pressed)
            {
                DispatchOnButton(i, type, State.Released);
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
                DispatchOnStick(i, inputStick, InputDirection.Right, State.Down);
            }
            else if (current.X < 0 && prev.X == 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Left, State.Down);
            }
            else if (current.X > 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Right, State.Pressed);
            }
            else if (current.X < 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Left, State.Pressed);
            }
            else if (current.X == 0 && prevState[i].IsConnected && prev.X > 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Right, State.Released);
            }
            else if (current.X == 0 && prevState[i].IsConnected && prev.X < 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Left, State.Released);
            }

            if (current.Y > 0 && prev.Y == 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Up, State.Down);
            }
            else if (current.Y < 0 && prev.Y == 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Down, State.Down);
            }
            else if (current.Y > 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Up, State.Pressed);
            }
            else if (current.Y < 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Down, State.Pressed);
            }
            else if (current.Y == 0 && prevState[i].IsConnected && prev.Y > 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Up, State.Released);
            }
            else if (current.Y == 0 && prevState[i].IsConnected && prev.Y < 0)
            {
                DispatchOnStick(i, inputStick, InputDirection.Down, State.Released);
            }
        }

        private void HandleTriggers(int i)
        {
            HandleTrigger(i, Trigger.Left, state[i].Triggers.Left, prevState[i].Triggers.Left);
            HandleTrigger(i, Trigger.Right, state[i].Triggers.Right, prevState[i].Triggers.Right);
        }

        private void HandleTrigger(int i, Trigger type, float trigger, float prevTrigger)
        {
            if (trigger > 0 && prevTrigger == 0)
            {
                DispatchOnTrigger(i, type, State.Down);
            }
            else if (trigger > 0)
            {
                DispatchOnTrigger(i, type, State.Pressed);
            }
            else if (trigger == 0 && prevState[i].IsConnected && prevTrigger > 0)
            {
                DispatchOnTrigger(i, type, State.Released);
            }
        }
        #endregion
    }

}
