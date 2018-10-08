using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    BUTTON,
}

public class InputManager : MonoBehaviour
{
    KeyCode[] player_one;
    KeyCode[] player_two;

    private void Start()
    {
        player_one = new KeyCode[5]
        {
            KeyCode.W,
            KeyCode.S,
            KeyCode.A,
            KeyCode.D,
            KeyCode.LeftShift,
        };
        player_two = new KeyCode[5]
        {
            KeyCode.UpArrow,
            KeyCode.DownArrow,
            KeyCode.LeftArrow,
            KeyCode.RightArrow,
            KeyCode.RightShift,
        };
    }

    public KeyCode getPlayerKey(InputType type, int player)
    {
        switch (player)
        {
            case 0:
                return player_one[(int)type];
            case 1:
                return player_two[(int)type];
            default:
                return KeyCode.Escape;
        }
    }
}
