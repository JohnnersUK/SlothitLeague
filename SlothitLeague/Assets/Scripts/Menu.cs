using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    private bool selectedMenuOption;

    int numberOfGoals = 5;

    public KeyCode Forward;
    public KeyCode Back;
    public KeyCode jumpKey;
    public KeyCode Left;
    public KeyCode Right;

    public GameObject play;
    public GameObject numOfGoalsObject;

    public GameObject scoreToWinObject; //the objetc that hold the number of goals after the scene switches

    public string sceneToLoad;

    // Use this for initialization
    void Start () {
        selectedMenuOption = true;
        play.GetComponent<MenuOptionScript>().selected = true;
        numOfGoalsObject.GetComponent<Text>().text = "Goals:" + numberOfGoals;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(Forward))
        {
            selectedMenuOption = true;

            play.GetComponent<MenuOptionScript>().selected = true;
            numOfGoalsObject.GetComponent<MenuOptionScript>().selected = false;
        }


        if (Input.GetKey(Back))
        {
            selectedMenuOption = false;


            play.GetComponent<MenuOptionScript>().selected = false;
            numOfGoalsObject.GetComponent<MenuOptionScript>().selected = true;
        }


        if (Input.GetKeyUp(jumpKey) && selectedMenuOption)
        {
            scoreToWinObject.GetComponent<ScoreToWin>().score_to_win = numberOfGoals;
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
        else if(Input.GetKeyUp(jumpKey))
        {
            if(numberOfGoals < 10)
            {
                numberOfGoals++;

               
            }
            else
            {
                numberOfGoals = 5;
            }

            numOfGoalsObject.GetComponent<Text>().text = "Number of goals:" + numberOfGoals;
        }


    }
}

