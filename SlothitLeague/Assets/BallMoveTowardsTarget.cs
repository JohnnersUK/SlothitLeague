using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMoveTowardsTarget : MonoBehaviour
{
    private Vector3 Target;
    public float speed;

    private bool hit;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (!hit)
	    {
	        transform.position = Vector3.Lerp(transform.position, Target, Time.deltaTime * speed);
	        if (transform.position == Target)
	            hit = true;
	    }
	}


    public void SetMoveTarget(Vector3 tar)
    {
        Target = tar;
        hit = false;
    }

}
