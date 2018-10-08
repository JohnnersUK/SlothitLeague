using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] int player_index;              //players index
    [SerializeField] SpriteRenderer m_renderer;     //this objects renderer
    [SerializeField] GameManager game_manager;

    private void Start()
    {
        //set the player and the goal to the appropriate team colour
        m_renderer.color = game_manager.getPlayerColour(player_index);
    }

    public int getIndex()
    {
        return player_index;
    }
}
