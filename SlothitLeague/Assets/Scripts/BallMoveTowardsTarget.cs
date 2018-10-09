using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMoveTowardsTarget : MonoBehaviour
{
    [SerializeField] InputManager input;
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

    Vector2 direction = Vector2.zero;
    [SerializeField] float def_speed;
    float speed;
    public bool hit;
    bool DoOnce;
    bool done;
    int hits;

    private Color defualtCol;

    public float aimMaxY;
    public float aimMinY;

    public float kickoff_dist_modifier;

    public float normalHitPowerModifier;

    private int kickoff_player_index = -1;

    float[] player_hit_timer;   //time since each player last hit the ball

    // Use this for initialization
    void Start()
    {
        Aimer.SetActive(false);
        hits = -1;

        defualtCol = GetComponent<SpriteRenderer>().color;

        player_hit_timer = new float[2];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < player_hit_timer.Length; i++)
        {
            player_hit_timer[i] -= Time.deltaTime;
        }
        if (!Aimer.activeSelf)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            if (speed > def_speed)
            {
                speed -= Time.deltaTime;
            }
            else
            {
                speed = def_speed;
            }
        }

        if (targeting && !end)
        {
            moverTimer += Time.deltaTime;

            if (moverTimer > timeToMoveAim)
            {
                if (Aimer.transform.position.y <= aimMaxY && !goDown)
                {
                    Aimer.transform.position += new Vector3(0, 1, 0);
                }
                else
                {
                    goDown = true;
                    if (Aimer.transform.position.y >= aimMinY)
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

            if (Input.GetKeyDown(input.getPlayerKey(InputType.BUTTON, kickoff_player_index)))
            {
                end = true;
            }
        }

        if (end)
        {
            if (!DoOnce)
            {
                Vector2 d = Aimer.transform.position - transform.position;
                d.Normalize();
                setDirection(d);
            }

            foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
                player.GetComponent<Control>().SetCanMove(true);

            DoOnce = true;
            Aimer.SetActive(false);
        }
    }


    public void setDirection(Vector2 _direction)
    {
        speed = def_speed * 2.5f;
        direction = _direction;
        hit = false;
    }

    public void flipDir(bool x)
    {
        if (x)
            direction.x *= -1;
        else
            direction.y *= -1;
    }

    public void Stop()
    {
        direction = Vector2.zero;
        hit = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GetComponent<SpriteRenderer>().color = col.gameObject.GetComponent<SpriteRenderer>().color;

            hits++;
            if (hits > 1)
            {
                hit = true;
                done = true;

                if (col.gameObject.tag == "Player")
                {
                    int player_index = col.gameObject.GetComponent<PlayerData>().getIndex();
                    if (player_hit_timer[player_index] <= 0)
                    {
                        Vector2 d = transform.position - col.transform.position;
                        d.Normalize();
                        setDirection(d);
                        player_hit_timer[player_index] = 0.5f;
                    }
                }
            }

            if (col.gameObject.tag == "Player" && !DoOnce)
            {
                Stop();

                hitMoveToDist = col.gameObject.GetComponent<Control>().GetSpeed();

                float dir = col.gameObject.transform.InverseTransformDirection(Vector3.up).x;

                Aimer.SetActive(true);

                kickoff_player_index = col.gameObject.GetComponent<PlayerData>().getIndex();

                col.gameObject.GetComponent<Control>().DisableBoost();

                if (dir > 0)
                {
                    if (col.gameObject.transform.position.x > transform.position.x)
                    {
                        Aimer.transform.position =
                            new Vector2(transform.position.x - (hitMoveToDist * kickoff_dist_modifier),
                                Aimer.transform.position.y);
                    }
                }
                else
                {
                    Aimer.transform.position =
                        new Vector2(transform.position.x + (hitMoveToDist * kickoff_dist_modifier),
                            Aimer.transform.position.y);
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

                //                                                                                          HERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
                    player.GetComponent<Control>().SetCanMove(false);

                

                targeting = true;
                SelectEndTimer = 0;
                moverTimer = 0;
                end = false;
            }
        }

        if (col.gameObject.name == "LeftConstraint" && direction.x < 0)
        {
            flipDir(true);
        }

        if (col.gameObject.name == "RightConstraint" && direction.x > 0)
        {
            flipDir(true);
        }

        if (col.gameObject.name == "BottomConstraint" && direction.y < 0)
        {
            flipDir(false);
        }

        if (col.gameObject.name == "TopConstraint" && direction.y > 0)
        {
            flipDir(false);
        }
    }

    public void SlowMo()
    {
        speed *= 0.5f;
    }

    public void Reset()
    {
        GetComponent<SpriteRenderer>().color = defualtCol;

        SelectEndTimer = 0;
        moverTimer = 0;

        targeting = false;
        end = false;

        hit = false;
        done = false;
        DoOnce = false;

        hits = -1;

        kickoff_player_index = -1;
    }
}
