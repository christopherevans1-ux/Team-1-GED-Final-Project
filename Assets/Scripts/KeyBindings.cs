using UnityEngine;

public enum GameAction
{
    MoveLeft,
    MoveRight,
    Jump,
    Pause,
    Restart,
    Undo
}
public static class KeyBindings
{
    public static KeyCode MoveLeft = KeyCode.A;
    public static KeyCode MoveRight = KeyCode.D;
    public static KeyCode Jump = KeyCode.Space;
    public static KeyCode Pause = KeyCode.Escape;
    public static KeyCode Restart = KeyCode.R;
    public static KeyCode Undo = KeyCode.LeftArrow;

    public static KeyCode GetKey(GameAction action)
    {
        switch (action)
        {
            case GameAction.MoveLeft:
                return MoveLeft;
            case GameAction.MoveRight:
                return MoveRight;
            case GameAction.Jump:
                return Jump;
            case GameAction.Pause: 
                return Pause;
            case GameAction.Restart:
                return Restart;
            case GameAction.Undo:
                return Undo;

            default:
                return KeyCode.None;
        }
    }

    public static void SetKey(GameAction action, KeyCode newKey)
    {
        switch(action)
        {
            case GameAction.MoveLeft:
                MoveLeft = newKey;
                break;
            case GameAction.MoveRight:
                MoveRight = newKey;
                break;
            case GameAction.Jump:
                Jump = newKey;
                break;
            case GameAction.Pause:
                Pause = newKey;
                break;
            case GameAction.Restart:
                Restart = newKey;
                break;
            case GameAction.Undo:
                Undo = newKey;
                break;
        }
    }
}
