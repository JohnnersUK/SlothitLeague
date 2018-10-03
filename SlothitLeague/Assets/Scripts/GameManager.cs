﻿using System.Collections;
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

    private void Start()
    {
        //initialise scores to 0
        scores = new int[2] { 0, 0 };

        StartCoroutine(resetObjects());
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
            StartCoroutine(resetObjects());
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
        //disable player movement
        foreach(Control control in player_controllers)
        {
            control.enabled = false;
        }

        foreach(ResetTransform r in reset_transform)
        {
            r.resetTransform();
        }

        //wait
        yield return new WaitForSeconds(1.0f);

        //reactiveate player movement
        foreach(Control control in player_controllers)
        {
            control.enabled = true;
            control.resetSpeed();
        }

        yield return null;
    }
}