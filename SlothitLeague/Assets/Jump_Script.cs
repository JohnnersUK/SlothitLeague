using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Script : MonoBehaviour {

    public float maxJumpVel;
    public KeyCode jumpKey;

    float jumpVel;
    float jTime_max;
    float jTime;

    bool grounded;

    // Use this for initialization
    void Start ()
    {
		jTime_max = maxJumpVel / 10;
        jTime = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyUp(jumpKey) && grounded)
        {
            jTime = jTime_max;
        }

        if (jTime > 0)
        {
            grounded = false;
            processJump();
        }
        else
        {
            grounded = true;
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        jTime -= 1 * Time.deltaTime;
    }

    void processJump()
    {
        float jumpRate = 1.0f * Time.deltaTime;

        if (jTime > (jTime_max/2))
        {
            transform.localScale += transform.localScale * jumpRate;
        }
        else if (jTime < (jTime_max / 2))
        {
            transform.localScale -= transform.localScale * jumpRate;
        }
    }

    public bool getGrounded()
    {
        return grounded;
    }
}
