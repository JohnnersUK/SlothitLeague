﻿/*
 * Stores all input keycodes for each player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//names of the inputs
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

    /// <summary>
    /// Returns the key that maps to a player's action
    /// </summary>
    /// <param name="type">The action to check</param>
    /// <param name="player">The player to check</param>
    /// <returns></returns>
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
