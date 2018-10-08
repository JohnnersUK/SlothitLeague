using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{

    public KeyCode Forward;
    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Back;
    public KeyCode Boost;

    public float turnspeed;
    public float acceleration;
    public float deceleration;
    public float maxSpeed;
    public float boostForce;

    float speed;
    bool reversing = false;
    float turnTimer = 0;
    bool canBoost = true;

    public int noBoosts = 1;

    private bool CanMove = true;

    public bool goingForward;

    public bool goalRight;

	// Use this for initialization
	void Start () {
        //Screen.SetResolution(256, 192, true);
        //Screen.SetResolution(640, 480, true);
        //Screen.SetResolution(512, 384, true);
        //Screen.SetResolution(342, 256, true);
        resetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            if (Input.GetKey(Forward))
            {
                goingForward = true;
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
                goingForward = false;
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

            if (Input.GetKey(Boost))
            {
                if (noBoosts > 0 && canBoost && CanMove)
                {
                    speed += boostForce;
                    noBoosts--;
                    canBoost = false;
                }
            }

            if (Input.GetKeyUp(Boost))
            {
                canBoost = true;
            }

            if (speed > maxSpeed)
            {
                speed -= deceleration * Time.deltaTime;
            }

            this.transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    public void SetCanMove(bool _canMove)
    {
        CanMove = _canMove;
    }

    public int GetSpeed()
    {
        return (int)speed;
    }

    public void resetSpeed()
    {
        speed = 0;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pickup") && noBoosts < 3)
        {
            noBoosts++;
            other.gameObject.transform.localPosition += new Vector3(1000, 1000, 1000);
        }

      
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == ("Player"))
        {

            Debug.Log("col");
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();

            Vector2 dir = other.gameObject.transform.position - transform.position;
            dir.Normalize();

            rb.velocity = Vector2.zero;

            rb.AddForce(dir * 50 * (1 + speed));
            speed = -speed;
            reversing = true;
        }

        if (other.gameObject.name == "LeftConstraint")
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            speed = -speed;
            rb.AddForce(Vector2.right * 200);
            reversing = true;
        }
        if (other.gameObject.name == "RightConstraint")
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            speed = -speed;
            rb.AddForce(Vector2.left * 200);
            reversing = true;
        }
        if (other.gameObject.name == "TopConstraint")
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            speed = -speed;
            rb.AddForce(Vector2.down * 200);
            reversing = true;
        }
        if (other.gameObject.name == "BottomConstraint")
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            speed = -speed;
            rb.AddForce(Vector2.up * 200);
            reversing = true;
        }
    }
        

      
    
}
    
