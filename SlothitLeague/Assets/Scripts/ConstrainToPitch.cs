using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainToPitch : MonoBehaviour
{
    [SerializeField] Vector2[] constraints; //constraints of the map
   // [SerializeField] BallMoveTowardsTarget ball;
    [SerializeField] Control[] players;
    bool stuck = false; //checks whether the ball has been out of bounds for more than two frames

    private void Update()
    {
        //constrainBall();
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

    //void constrainBall()
    //{
    //    if (ball.transform.position.x < constraints[0].x)
    //    {
    //        ball.transform.position = new Vector3(constraints[0].x, ball.transform.position.y);
    //        if (!stuck)
    //        {
    //            ball.flipDir(true);
    //            stuck = true;
    //        }
    //        else
    //        {
    //            ball.setDirection(Vector2.right);
    //        }
    //    }
    //    else if (ball.transform.position.x > constraints[1].x)
    //    {
    //        ball.transform.position = new Vector3(constraints[1].x, ball.transform.position.y);
    //        if (!stuck)
    //        {
    //            ball.flipDir(true);
    //            stuck = true;
    //        }
    //        else
    //        {
    //            ball.setDirection(Vector2.left);
    //        }
    //    }
    //    else if (ball.transform.position.y < constraints[0].y)
    //    {
    //        ball.transform.position = new Vector3(ball.transform.position.x, constraints[0].y);
    //        if (!stuck)
    //        {
    //            ball.flipDir(false);
    //            stuck = true;
    //        }
    //        else
    //        {
    //            ball.setDirection(Vector2.up);
    //        }
    //    }
    //    else if (ball.transform.position.y > constraints[1].y)
    //    {
    //        ball.transform.position = new Vector3(ball.transform.position.x, constraints[1].y);
    //        if (!stuck)
    //        {
    //            ball.flipDir(false);
    //            stuck = true;
    //        }
    //        else
    //        {
    //            ball.setDirection(Vector2.down);
    //        }
    //    }
    //    else if (stuck)
    //    {
    //        stuck = false;
    //    }
    //}
}
