using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] Control[] player_controllers;
    [SerializeField] Jump_Script[] jump_scripts;
    [SerializeField] GameObject[] goals;
    [SerializeField] GameManager score_manager;
    [SerializeField] Rigidbody2D rb;
    int players_on_ball = 0;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            int index = col.gameObject.GetComponent<PlayerData>().getIndex();
            Vector2 direction = (Vector2)transform.position - col.GetContact(0).point;
            direction.Normalize();
            playerCollide(index, direction);
        }
        else if (col.gameObject.tag == "Goal")
        {
            int index = col.gameObject.GetComponent<PlayerData>().getIndex();
            scoreGoal(index);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            players_on_ball--;
        }
    }

    /// <summary>
    /// Logic for when a player collides with a ball
    /// </summary>
    /// <param name="index">index of the player</param>
    void playerCollide(int index, Vector2 dir)
    {
        players_on_ball++;
        if(players_on_ball > 1)
        {
            int x = Random.Range(-1, 2);
            int y = Random.Range(-1, 2);
            Vector2 force = new Vector2(x,y);
            force.Normalize();
            rb.AddForce(force * 5);
        }
        else
        {
            rb.AddForce(dir * 5);
        }
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
