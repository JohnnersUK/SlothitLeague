using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{

    public KeyCode Forward;
    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Back;
    public float turnspeed;
    public float acceleration;
    public float deceleration;
    public float maxSpeed;

    float speed;
    bool reversing = false;
    float turnTimer = 0;

    private bool CanMove = true;

    public bool goingForward;

    public bool goalRight;

	// Use this for initialization
	void Start () {
        //Screen.SetResolution(256, 192, true);
        //Screen.SetResolution(640, 480, true);
        //Screen.SetResolution(512, 384, true);
        Screen.SetResolution(342, 256, true);
    }
	
	// Update is called once per frame
	void Update () {
	  if (CanMove)
	  {
		if ( Input.GetKey(Forward))
        {
            if (speed < maxSpeed)
            {
                reversing = false;
                speed += acceleration * Time.deltaTime;
            }
        }
        else if (speed > 0 && !reversing)
        {
            speed -= deceleration * Time.deltaTime;
        }
        if (speed < 0 && !reversing)
        {
            speed = 0;
        }

	            if (speed < maxSpeed)
	            {
	                reversing = false;
	                speed += acceleration;
	            }
	        }
	        else if (speed > 0 && !reversing)
	        {
	            speed -= deceleration;
	        }


        if (Input.GetKey(Left))
        {
            turnTimer += Time.deltaTime * 100;
            if (turnTimer > 100 / turnspeed)
            {
                this.transform.Rotate(new Vector3(0, 0, 15));
                turnTimer = 0;
            }
        }
        if (Input.GetKey(Right))
        {
            turnTimer += Time.deltaTime * 100;
            if (turnTimer > 100 / turnspeed)
            {
                transform.Rotate(new Vector3(0, 0, -15));
                turnTimer = 0;
            }
        }

        if (Input.GetKey(Back))
        {
            if (speed > (-maxSpeed / 2) && speed <= 0)
            {
                reversing = true;
                speed -= acceleration * Time.deltaTime;
            }
            else if (speed > 0 && !reversing)
            {
                speed -= (deceleration * Time.deltaTime) * 2;
            }
        }
        else if (speed < 0 && reversing)
        {
            speed += deceleration * Time.deltaTime;
        }
        if (speed > 0 && reversing)
        {
            speed = 0;
        }
        this.transform.position += transform.up * speed * Time.deltaTime;
	}
    }

    public void resetSpeed()
    {
        speed = 0;
    }
}
