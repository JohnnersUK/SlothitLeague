using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] Control[] player_controllers;
    [SerializeField] Jump_Script[] jump_scripts;
    [SerializeField] GameObject[] goals;
    [SerializeField] GameManager score_manager;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            int index = col.gameObject.GetComponent<PlayerData>().getIndex();
            playerCollide(index);
        }
        else if (col.gameObject.tag == "Goal")
        {
            int index = col.gameObject.GetComponent<PlayerData>().getIndex();
            scoreGoal(index);
        }
    }

    /// <summary>
    /// Logic for when a player collides with a ball
    /// </summary>
    /// <param name="index">index of the player</param>
    void playerCollide(int index)
    {
        if(jump_scripts[index].getGrounded() != /*this object is grounded*/ true)
        {
            return;
        }
        for (int i = 0; i < player_controllers.Length; i++)
        {
            if (index != i)
            {
                player_controllers[i].enabled = false;
            }
        }

        //begin minigame
        //when minigame ends, enable player controllers again
    }

    /// <summary>
    /// Logic for when the ball collides with a goal
    /// </summary>
    /// <param name="index">The index of the goal</param>
    void scoreGoal(int index)
    {
        if (player_controllers.Length != 2)
        {
            Debug.LogError("This code may not work with more than two players");
        }
        score_manager.ModifyScore(1 - index, 1);
    }
}
