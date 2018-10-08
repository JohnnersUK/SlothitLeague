using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text[] ui_scores;  //objects that show the scores on the UI
    int[] scores;                       //current score for each player

    [SerializeField] int score_to_win;  //number of points needed to win
    [SerializeField] Text win_text;     //text object that displays on game over
    
    [SerializeField] ResetTransform[] reset_transform;  //objects to reset when goals are scored

    [SerializeField] Control[] player_controllers;  //each player's controller

    [SerializeField] GameObject ball;

    float resetTimer;
    bool timerTrigger;
    bool countdownReset;
   

    // JS animation
 
    public Animator ballAnimator;
    public Animator sceneAnimator;

    //[HideInInspector]
    public AudioManager audioManager;

    
    private void Start()
    {
        //initialise scores to 0
        scores = new int[2] { 0, 0 };
        GameObject scoresToWIn = GameObject.FindGameObjectWithTag("ScoreToWin");
        score_to_win = scoresToWIn.GetComponent<ScoreToWin>().score_to_win;
        StartCoroutine(resetObjects());
        timerTrigger = false;
        countdownReset = false;

        //audioManager.Play("Game Start");
    }

    private void Awake()
    {
        //starts the "animation + sounds if statements" just after the reset
        timerTrigger = true;
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
       
            
            if(countdownReset == false)
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

            // Sets "score" bool to true to trigger the explosion animation
            ballAnimator.SetBool("Goal Scored", false);
            sceneAnimator.SetBool("Reset", true);

            StartCoroutine(resetObjects());

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
            sceneAnimator.SetBool("Reset", false);

            // turn engine noise back on
            audioManager.Play("Engine Noise");

            // reinitilize 
            countdownReset = false;
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
    }

    /// <summary>
    /// Sends the players back to the starting positions
    /// </summary>
    /// <returns></returns>
    IEnumerator resetObjects()
    {
        ball.GetComponent<BallMoveTowardsTarget>().Reset();
        //disable player movement
        foreach (Control control in player_controllers)
        {
            control.enabled = false;
        }

        foreach (ResetTransform r in reset_transform)
        {
            r.resetTransform();
        }

        //wait
        //yield return new WaitForSeconds(5.0f); // broke
        

        
            //reactiveate player movement
            foreach (Control control in player_controllers)
            {
                control.enabled = true;
                control.resetSpeed();
            }

        yield return null;
    }

    
    
}
