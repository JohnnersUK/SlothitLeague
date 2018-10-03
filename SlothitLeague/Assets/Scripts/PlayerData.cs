using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] int player_index;              //players index
    [SerializeField] Color[] player_colours;        //all sprite colours
    [SerializeField] SpriteRenderer m_renderer;     //this objects renderer

    private void Start()
    {
        //set the player and the goal to the appropriate team colour
        m_renderer.color = player_colours[player_index];
    }

    public int getIndex()
    {
        return player_index;
    }
}
