using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTransform : MonoBehaviour
{
    int target_index = 0;
    [SerializeField] Transform[] all_targets;

    /// <summary>
    /// resets the position, rotation, and scale of this object
    /// to allow for another play
    /// </summary>
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
