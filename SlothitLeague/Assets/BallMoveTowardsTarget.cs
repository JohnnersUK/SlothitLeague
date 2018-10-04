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
    bool done;
    int hits;

    public float normalHitPowerModifier;

	// Use this for initialization
	void Start ()
	{
	    Aimer.SetActive(false);
	    hits = -1;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (!hit && hits > -1)
	    {
            if (!done)
            {
                transform.position = Vector3.Lerp(transform.position, Target, Time.deltaTime * speed);
                if (hits > 0)
                {
                    done = true;
                }
            }
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

    /// <summary>
    /// Reflects the target in an axis, so the ball
    /// goes the same distance in the other direction
    /// </summary>
    /// <param name="x">is it the x axis</param>
    public void flipTarget(bool x)
    {
        if (x)
        {
            float dist_to_target = Target.x - transform.position.x;
            Target -= new Vector3(dist_to_target * 2, 0);
        }
        else
        {
            float dist_to_target = Target.y - transform.position.y;
            Target -= new Vector3(0, dist_to_target * 2);
        }
    }

    public void Stop()
    {
        Target = transform.position;
        hit = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        hits++;
        if (hits > 1)
        {
            hit = true;
            done = true;

            if (col.gameObject.tag == "Player")
            {
                if (col.gameObject.GetComponent<Control>().GetSpeed() < 1)
                {
                    GetComponent<Rigidbody2D>().AddForce((transform.position - col.transform.position) *
                                                         normalHitPowerModifier);
                }
                else
                {
                    GetComponent<Rigidbody2D>().AddForce((transform.position - col.transform.position) *
                                                         col.gameObject.GetComponent<Control>().GetSpeed() *
                                                         normalHitPowerModifier);
                }
            }
        }
        if (col.gameObject.tag == "Player" && !DoOnce)
        {
            Stop();

            Debug.Log(col.gameObject.transform.InverseTransformDirection(Vector3.up).ToString());

            hitMoveToDist = col.gameObject.GetComponent<Control>().GetSpeed();

            float dir = col.gameObject.transform.InverseTransformDirection(Vector3.up).x;

            Aimer.SetActive(true);

            if (dir > 0)
            {
                if (col.gameObject.transform.position.x > transform.position.x)
                {
                    Aimer.transform.position =
                            new Vector2(transform.position.x - hitMoveToDist, Aimer.transform.position.y);
                }
            }
            else
            {
                Aimer.transform.position =
                    new Vector2(transform.position.x + hitMoveToDist, Aimer.transform.position.y);
            }

            if (Aimer.transform.position.x > RightLimit)
            {
                Aimer.transform.position =
                    new Vector2(RightLimit, Aimer.transform.position.y);
            }

            if (Aimer.transform.position.x < LeftLimit)
            {
                Aimer.transform.position =
                    new Vector2(LeftLimit, Aimer.transform.position.y);
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


    public void Reset()
    {
        SelectEndTimer = 0;
        moverTimer = 0;

        targeting = false;
        end = false;

    hit = false;
        done = false;
        DoOnce = false;

        hits = -1;

    }
}
