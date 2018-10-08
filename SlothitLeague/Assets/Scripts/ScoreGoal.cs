using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGoal : MonoBehaviour
{
    [SerializeField] Control[] player_controllers;
    [SerializeField] GameObject[] goals;
    [SerializeField] GameManager score_manager;
    [SerializeField] BallMoveTowardsTarget ball;
    int players_on_ball = 0;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Goal")
        {
            int index = col.gameObject.GetComponent<PlayerData>().getIndex();
            scoreGoal(index);
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
        ball.setDirection(Vector2.zero);
    }
}
