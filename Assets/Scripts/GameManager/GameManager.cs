using UnityEngine;
using InputManager;

public class GameManager : MonoBehaviour
{
    #region Variables
    PlayerInput playerInput;
    #endregion

    #region Mono Methods
    private void Awake()
    {
        InputSystem.InitInputMonitor();
        playerInput = new PlayerInput();
    }

    private void Update()
    {
        InputSystem.Update();
    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
        
    }
    #endregion
}
 