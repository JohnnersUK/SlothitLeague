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
    bool reversing = false;
    float turnTimer = 0;
    bool canBoost = true;

    public int noBoosts = 1;

    private bool CanMove = true;

    public bool goingForward;

    public bool goalRight;

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

            if (Input.GetKey(buttons[(int)InputType.DOWN]))
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

            if (Input.GetKey(buttons[(int)InputType.BUTTON]))
            {
                if (noBoosts > 0 && canBoost && CanMove)
                {
                    speed += boostForce;
                    noBoosts--;
                    canBoost = false;
                }
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
}
