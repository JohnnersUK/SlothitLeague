/*
 * Sends the object to where it needs to go when a player scores a goal
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTransform : MonoBehaviour
{
    int target_index = 0;                       //target to move to next
    [SerializeField] Transform[] all_targets;   //all targets object can move to

    /// <summary>
    /// sends the object to one of its starting positions
    /// </summary>
    /// <param name="winner">The player who scored, -1 if resetting for another reason</param>
    public void resetTransform(int winner = -1)
    {
        Rigidbody2D rb;
        if (rb = GetComponent<Rigidbody2D>())
        {
            rb.velocity = Vector2.zero;
        }
        if (winner != -1)
        {
            target_index = winner;
        }
        transform.position = all_targets[target_index].position;
        transform.localRotation = all_targets[target_index].localRotation;
    }
}
