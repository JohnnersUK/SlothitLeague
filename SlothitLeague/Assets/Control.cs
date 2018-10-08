using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField] InputManager input;
    [SerializeField] PlayerData data;

    KeyCode[] buttons;

    public float turnspeed;
    public float acceleration;
    public float deceleration;
    public float maxSpeed;
    public float boostForce;

    float speed;
    //bool reversing = false;
    float turnTimer = 0;
    bool canBoost = true;

    public int noBoosts = 1;

    private bool CanMove = true;

    public bool goalRight;

    bool boostUnavailable = false;

    // Use this for initialization
    void Start()
    {
        int index = data.getIndex();
        buttons = new KeyCode[5];
        for (int i = 0; i < 5; i++)
        {
            buttons[i] = input.getPlayerKey((InputType)i, index);
        }
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
            if (Input.GetKey(buttons[(int)InputType.UP]))
            {
                if (speed < maxSpeed)
                {
                    speed += acceleration * Time.deltaTime;
                }
            }
            else if (Input.GetKey(buttons[(int)InputType.DOWN]))
            {
                if (speed > (-maxSpeed / 2) && speed <= 0)
                {
                    speed -= acceleration * Time.deltaTime;
                }
                else if (speed > 0)
                {
                    speed -= (deceleration * Time.deltaTime) * 2;
                }
            }
            else
            {
                int decel_dir = speed > 0 ? 1 : -1;
                speed -= (deceleration * decel_dir * Time.deltaTime);
            }

            if (Input.GetKey(buttons[(int)InputType.LEFT]))
            {
                turnTimer += Time.deltaTime * 100;
                if (turnTimer > 100 / turnspeed)
                {
                    this.transform.Rotate(new Vector3(0, 0, 15));
                    turnTimer = 0;
                }
            }
            if (Input.GetKey(buttons[(int)InputType.RIGHT]))
            {
                turnTimer += Time.deltaTime * 100;
                if (turnTimer > 100 / turnspeed)
                {
                    transform.Rotate(new Vector3(0, 0, -15));
                    turnTimer = 0;
                }
            }

            if (Input.GetKeyDown(buttons[(int)InputType.BUTTON]))
            {
                if (noBoosts > 0 && canBoost && CanMove && !boostUnavailable)
                {
                    speed += boostForce;
                    noBoosts--;
                    canBoost = false;
                }
                boostUnavailable = false;
            }

            if (Input.GetKeyUp(buttons[(int)InputType.BUTTON]))
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
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();

            Vector2 dir = other.gameObject.transform.position - transform.position;

            dir.x = round(dir.x);
            dir.y = round(dir.y);
            dir.Normalize();

            rb.velocity = Vector2.zero;

            rb.AddForce(dir * 30 * (1 + speed));
            speed = 0;
        }

        if (other.gameObject.name == "LeftConstraint" ||
            other.gameObject.name == "RightConstraint" ||
            other.gameObject.name == "TopConstraint" ||
            other.gameObject.name == "BottomConstraint")
        {
            speed = -speed * 0.5f;
        }
    }

    int round(float f)
    {
        if (f < -0.2f)
            return -1;
        else if (f > 0.2f)
            return 1;
        else
            return 0;
    }

    public void DisableBoost()
    {
        boostUnavailable = true;
    }
}

