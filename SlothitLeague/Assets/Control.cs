﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetKey(Forward))
        {
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
        if (speed <0 && !reversing)
        {
            speed = 0;
        }
       


        if (Input.GetKey(Left))
        {
            turnTimer++;
            if (turnTimer > 100 / turnspeed)
            {
                this.transform.Rotate(new Vector3(0, 0, 15));
                turnTimer = 0;
            }
        }
        if(Input.GetKey(Right))
        {
            turnTimer++;
            if (turnTimer > 100 / turnspeed)
            {
                transform.Rotate(new Vector3(0, 0, -15));
                turnTimer = 0;
            }
        }

        if (Input.GetKey(Back))
        {
            if (speed < (-maxSpeed / 2))
            {
                reversing = true;
                speed -= acceleration;
            }
        }
        else if (speed < 0 && reversing)
        {
            speed += deceleration;
        }
        if (speed > 0 && reversing)
        {
            speed = 0;
        }
        this.transform.position += transform.up * speed * Time.deltaTime;
    }
}