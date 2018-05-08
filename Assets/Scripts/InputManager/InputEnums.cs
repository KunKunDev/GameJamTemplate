namespace InputManager
{
    public enum ControllerState
    {
        Connection,
        Disconnection
    }

    public enum InputButton
    {
        A,
        B,
        X,
        Y,
        LeftShoulder,
        RightShoulder,
        LeftStick,
        RightStick,
        Start,
        Back,
        Guide,
        UpArrow,
        DownArrow,
        RightArrow,
        LeftArrow
    }

    public enum InputStick
    {
        LeftStick,
        RightStick
    }

    public enum InputDirection
    {
        Up,
        Down,
        Right,
        Left
    }

    public enum InputTrigger
    {
        Left,
        Right
    }

    public enum InputState
    {
        Down,
        Pressed,
        Released
    }
}