using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMoveTowardsTarget : MonoBehaviour
{
    public KeyCode Select;

    public GameObject Aimer;

    private bool targeting;
    private bool end;

    private int hitMoveToDist = 1;
    private float moverTimer;

    public float timeToMoveAim;

    private bool goDown;

    private float SelectEndTimer;

    public float LeftLimit;
    public float RightLimit;


    private Vector3 Target;
    public float speed;
    public bool hit;

    bool DoOnce;

	// Use this for initialization
	void Start ()
	{
	    Aimer.SetActive(false);
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
	    else
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

	            SetMoveTarget(Aimer.transform.position);

	            foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
	                player.GetComponent<Control>().SetCanMove(true);

	            DoOnce = true;
               
	            Aimer.SetActive(false);
	        }
	    }
	}


    public void SetMoveTarget(Vector3 tar)
    {
        Target = tar;
        hit = false;
    }


    public void Stop()
    {
        Target = transform.position;
        hit = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && !DoOnce)
        {
            Stop();

            Debug.Log(col.gameObject.transform.InverseTransformDirection(Vector3.up).ToString());

            hitMoveToDist = col.gameObject.GetComponent<Control>().GetSpeed();

            float dir = col.gameObject.transform.InverseTransformDirection(Vector3.up).x;

            Aimer.SetActive(true);

            if (col.gameObject.GetComponent<Control>().goingForward == false)
                dir = -dir;

            if (dir > 0)
            {
                if (col.gameObject.GetComponent<Control>().goalRight == true)
                {
                    Aimer.transform.position =
                        new Vector2(transform.position.x + hitMoveToDist, Aimer.transform.position.y);
                }
                else
                {
                    Aimer.transform.position =
                        new Vector2(transform.position.x - hitMoveToDist, Aimer.transform.position.y);
                }
            }
            else
            {
                if (col.gameObject.GetComponent<Control>().goalRight == false)
                {
                    Aimer.transform.position =
                        new Vector2(transform.position.x - hitMoveToDist, Aimer.transform.position.y);
                }
                else
                {
                    Aimer.transform.position =
                        new Vector2(transform.position.x + hitMoveToDist, Aimer.transform.position.y);
                }
            }

            if (col.transform.position.x > RightLimit)
            {
                Aimer.transform.position =
                    new Vector2(col.transform.position.x - hitMoveToDist, Aimer.transform.position.y);
            }

            if (col.transform.position.x < LeftLimit)
            {
                Aimer.transform.position =
                    new Vector2(col.transform.position.x + hitMoveToDist, Aimer.transform.position.y);
            }

            col.gameObject.GetComponent<Control>().SetCanMove(false);

            foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
                player.GetComponent<Control>().SetCanMove(false);

            targeting = true;
            SelectEndTimer = 0;
            moverTimer = 0;
            end = false;
        }
    }
}
