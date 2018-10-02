using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHitAiming : MonoBehaviour
{
    public KeyCode Select;
    public GameObject Aimer;
    public GameObject Ball;

    private bool targeting;
    private bool end;

    private float moverTimer;

    public float timeToMoveAim;

    private bool goDown;

    private float SelectEndTimer;


	// Use this for initialization
	void Start ()
	{
	    Aimer.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (targeting && !end)
	    {
	        moverTimer += Time.deltaTime;

	        if (moverTimer > timeToMoveAim)
	        {
	            if (Aimer.transform.position.y <= 3 && !goDown)
	            {
	                Aimer.transform.position += new Vector3(0, 1, 0);
                }
	            else
	            {
	                goDown = true;
	                if (Aimer.transform.position.y >= -3)
	                {
	                    Aimer.transform.position -= new Vector3(0, 1, 0);
	                }
	                else
	                {
	                    goDown = false;
	                }
	            }

	            moverTimer = 0;
	        }

	        if (Input.GetKeyDown(Select))
	            end = true;
	    }

	    if (end)
	    {
	       // SelectEndTimer += Time.deltaTime;

	        Ball.GetComponent<BallMoveTowardsTarget>().SetMoveTarget(Aimer.transform.position);
	        this.GetComponent<Control>().SetCanMove(true);

          //  if (SelectEndTimer > 2)
	        Aimer.SetActive(false);
	    }
	}


    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(transform.InverseTransformDirection(Vector3.up).ToString());

        float dir = transform.InverseTransformDirection(Vector3.up).x;

        if (GetComponent<Control>().goingForward == false)
            dir = -dir;

            if (dir < 0)
        {
            Aimer.transform.position = new Vector2(transform.position.x + 5, Aimer.transform.position.y);
        }
        else
        {
            Aimer.transform.position = new Vector2(transform.position.x - 5, Aimer.transform.position.y);
        }

        //  Aimer.transform.position = new Vector2(transform.position.x - 5, Aimer.transform.position.y);
        Aimer.SetActive(true);
        this.GetComponent<Control>().SetCanMove(false);
        targeting = true;
        SelectEndTimer = 0;
        moverTimer = 0;
        end = false;
    }
}
