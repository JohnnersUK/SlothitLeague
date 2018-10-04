using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainToPitch : MonoBehaviour
{
    [SerializeField] Vector2[] constraints;
    [SerializeField] BallMoveTowardsTarget ball;
    [SerializeField] Control[] players;

    private void Update()
    {
        if (ball)
        {
            //Old version, needs updatting
            //if (ball.transform.position.x < constraints[0].x ||
            //    ball.transform.position.x < constraints[1].x)
            //{
            //    ball.flipTarget(true);
            //}
            //if (ball.transform.position.y < constraints[0].y ||
            //    ball.transform.position.y < constraints[1].y)
            //{
            //    ball.flipTarget(false);
            //}

            //copy of player method for now
            if (ball.transform.position.x < constraints[0].x)
            {
                ball.transform.position = new Vector3(constraints[0].x, ball.transform.position.y);
            }
            if (ball.transform.position.x > constraints[1].x)
            {
                ball.transform.position = new Vector3(constraints[1].x, ball.transform.position.y);
            }
            if (ball.transform.position.y < constraints[0].y)
            {
                ball.transform.position = new Vector3(ball.transform.position.x, constraints[0].y);
            }
            if (ball.transform.position.y > constraints[1].y)
            {
                ball.transform.position = new Vector3(ball.transform.position.x, constraints[1].y);
            }
        }

        foreach (Control player in players)
        {
            if (player.transform.position.x < constraints[0].x)
            {
                player.resetSpeed();
                player.transform.position = new Vector3(constraints[0].x, player.transform.position.y);
            }
            if (player.transform.position.x > constraints[1].x)
            {
                player.resetSpeed();
                player.transform.position = new Vector3(constraints[1].x, player.transform.position.y);
            }
            if (player.transform.position.y < constraints[0].y)
            {
                player.resetSpeed();
                player.transform.position = new Vector3(player.transform.position.x, constraints[0].y);
            }
            if (player.transform.position.y > constraints[1].y)
            {
                player.resetSpeed();
                player.transform.position = new Vector3(player.transform.position.x, constraints[1].y);
            }
        }
    }
}
