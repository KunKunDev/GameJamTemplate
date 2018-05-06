using UnityEngine;
using InputManager;

public class GameManager : MonoBehaviour
{
    #region Mono Methods
    private void Awake()
    {
        InputSystem.InitInputMonitor();
    }
    #endregion
}
