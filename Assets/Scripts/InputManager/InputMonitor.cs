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

        public void InitButtons(InputSystem.ButtonHandler onButtonADown)
        {
            DispatchOnButton = onButtonADown;
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
                }
            }
        }
        #endregion

        #region Class Methods
        private void HandleButtons(int i)
        {
            //Button A
            if (state[i].Buttons.A == ButtonState.Pressed && prevState[i].Buttons.A == ButtonState.Released)
            {
                DispatchOnButton(i, ButtonType.A, State.Down);
            }
            else if (state[i].Buttons.A == ButtonState.Pressed)
            {
                DispatchOnButton(i, ButtonType.A, State.Pressed);
            }
            else if (state[i].Buttons.A == ButtonState.Released && prevState[i].IsConnected && prevState[i].Buttons.A == ButtonState.Pressed)
            {
                DispatchOnButton(i, ButtonType.A, State.Released);
            }

            //Button B
            if (state[i].Buttons.B == ButtonState.Pressed && prevState[i].Buttons.B == ButtonState.Released)
            {
                DispatchOnButton(i, ButtonType.B, State.Down);
            }
            else if (state[i].Buttons.B == ButtonState.Pressed)
            {
                DispatchOnButton(i, ButtonType.B, State.Pressed);
            }
            else if (state[i].Buttons.B == ButtonState.Released && prevState[i].IsConnected && prevState[i].Buttons.B == ButtonState.Pressed)
            {
                DispatchOnButton(i, ButtonType.B, State.Released);
            }

            //Button X
            if (state[i].Buttons.X == ButtonState.Pressed && prevState[i].Buttons.X == ButtonState.Released)
            {
                DispatchOnButton(i, ButtonType.X, State.Down);
            }
            else if (state[i].Buttons.X == ButtonState.Pressed)
            {
                DispatchOnButton(i, ButtonType.X, State.Pressed);
            }
            else if (state[i].Buttons.X == ButtonState.Released && prevState[i].IsConnected && prevState[i].Buttons.X == ButtonState.Pressed)
            {
                DispatchOnButton(i, ButtonType.X, State.Released);
            }

            //Button Y
            if (state[i].Buttons.Y == ButtonState.Pressed && prevState[i].Buttons.Y == ButtonState.Released)
            {
                DispatchOnButton(i, ButtonType.Y, State.Down);
            }
            else if (state[i].Buttons.Y == ButtonState.Pressed)
            {
                DispatchOnButton(i, ButtonType.Y, State.Pressed);
            }
            else if (state[i].Buttons.Y == ButtonState.Released && prevState[i].IsConnected && prevState[i].Buttons.Y == ButtonState.Pressed)
            {
                DispatchOnButton(i, ButtonType.Y, State.Released);
            }
        }
        #endregion
    }

}
