using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text[] ui_scores;                  //objects that show the scores on the UI
    int[] scores;                                       //current score for each player
    [SerializeField] int score_to_win;                  //number of points needed to win
    [SerializeField] Text win_text;                     //text object that displays on game over   
    [SerializeField] ResetTransform[] reset_transform;  //objects to reset when goals are scored
    [SerializeField] Control[] player_controllers;      //each player's controller
    [SerializeField] GameObject ball;                   //the ball
    [SerializeField] Color[] player_colours;            //each player's team colour
    [SerializeField] InputManager input;                //the input manager

    float resetTimer;
    bool timerTrigger;
    bool countdownReset;

    int playerScore;

    // JS animation

    public Animator ballAnimator;
    public Animator sceneAnimator;

    //[HideInInspector]
    public AudioManager audioManager;


    private void Start()
    {
        //initialise scores to 0
        scores = new int[2] { 0, 0 };
        //GameObject scoresToWIn = GameObject.FindGameObjectWithTag("ScoreToWin");
        //score_to_win = scoresToWIn.GetComponent<ScoreToWin>().score_to_win;
        StartCoroutine(resetObjects());
        timerTrigger = true;
        countdownReset = false;
        playerScore = 0;
    }

    private void Awake()
    {
        //starts the "animation + sounds if statements" just after the reset
        //timerTrigger = true;
        resetTimer = 1.001f;
        // stop player movement
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
            player.GetComponent<Control>().SetCanMove(false);

        audioManager.Play("Engine Noise");
        audioManager.Mute("Engine Noise", true);

    }


    public void Update()
    {

        //animation + sound if statements
        // Ball Scored
        if (timerTrigger == true)
        {
            // start timer
            resetTimer += Time.deltaTime;

            // stop player movement
            foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
                player.GetComponent<Control>().SetCanMove(false);

            ball.GetComponent<BallMoveTowardsTarget>().setDirection(Vector2.zero);


            if (countdownReset == false)
            {
                // Sets "score" bool to true to trigger the explosion animation
                ballAnimator.SetBool("Goal Scored", true);
                audioManager.Mute("Engine Noise", true);

            }

        }

        // reset ball
        if (resetTimer > 1 && countdownReset == false)
        {

            // reset object

            // 
            ballAnimator.SetBool("Goal Scored", false);

            Debug.Log("TrueLog");
            sceneAnimator.SetBool("Reset", true);

            StartCoroutine(resetObjects(1 - playerScore));

            audioManager.Play("Game Start");

            countdownReset = true;

        }

        // start count in
        if (resetTimer > 4)
        {
            resetTimer = 0;
            timerTrigger = false;

            // allow player movement again 
            foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
                player.GetComponent<Control>().SetCanMove(true);

            // stop count in animation
            Debug.Log("Log");
            sceneAnimator.SetBool("Reset", false);

            // turn engine noise back on
            audioManager.Play("Engine Noise");

            // reinitilize 
            countdownReset = false;

            //if we have some win text up, clear it
            win_text.enabled = false;

            //make sure scores are displaying correctly
            ui_scores[0].text = scores[0].ToString();
            ui_scores[1].text = scores[1].ToString();
        }

    }
    /// <summary>
    /// Called when something changes the score
    /// </summary>
    /// <param name="player">Which player's score</param>
    /// <param name="change">What is the score being changed by</param>
    public void ModifyScore(int player, int change)
    {
        scores[player] += change;
        ui_scores[player].text = scores[player].ToString();

        //check win conditions
        if (scores[player] >= score_to_win)
        {
            endGame(player);
        }
        else
        {
            timerTrigger = true;
            playerScore = player;
        }
    }

    /// <summary>
    /// Begin game over state
    /// </summary>
    /// <param name="winner">Which player won</param>
    void endGame(int winner)
    {
        win_text.text = "Player " + (winner + 1).ToString() + " wins!";
        win_text.enabled = true;

        //reset the scores
        scores[0] = 0;
        scores[1] = 0;
        timerTrigger = true;
    }

    /// <summary>
    /// Sends the players back to the starting positions
    /// </summary>
    /// <returns></returns>
    IEnumerator resetObjects(int winner = -1)
    {
        ball.GetComponent<BallMoveTowardsTarget>().Reset();
        //disable player movement
        foreach (Control control in player_controllers)
        {
            control.enabled = false;
        }

        foreach (ResetTransform r in reset_transform)
        {
            r.resetTransform(winner);

        }

        //enable player movement
        foreach (Control control in player_controllers)
        {
            control.enabled = true;
            control.resetSpeed();
        }

        yield return null;
    }

    public Color getPlayerColour(int i)
    {
        return player_colours[i];
    }

}
