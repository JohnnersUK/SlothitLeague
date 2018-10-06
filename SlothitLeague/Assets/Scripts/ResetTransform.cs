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
    public void resetTransform()
    {
        Debug.Log(gameObject.name);
        transform.position = all_targets[target_index].position;
        transform.localRotation = all_targets[target_index].localRotation;
        transform.localScale = all_targets[target_index].localScale;

        //cycle through your targets if you have multiple
        target_index++;
        if(target_index >= all_targets.Length)
        {
            target_index = 0;
        }
    }
}
