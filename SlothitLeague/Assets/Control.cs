using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

    public KeyCode Forward;
    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Back;
    public float acceleration;
    public float deceleration;
    public float maxSpeed;

    float speed;
    bool reversing = false;

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
        else if (speed > 0)
        {
            speed -= deceleration;
        }
        if (speed <0 && !reversing)
        {
            speed = 0;
        }
       


        if (Input.GetKeyDown(Left))
        {
            this.transform.Rotate(new Vector3(0, 0, 15));
        }
        if(Input.GetKeyDown(Right))
        {
            transform.Rotate(new Vector3(0, 0, -15));
        }

        if (Input.GetKey(Back))
        {
            reversing = true;
            speed -= acceleration;
        }
        if (speed > 0 && reversing)
        {
            speed = 0;
        }
        this.transform.position += transform.up * speed * Time.deltaTime;
    }
}
