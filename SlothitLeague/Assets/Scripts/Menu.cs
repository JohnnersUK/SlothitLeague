using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    private bool selectedMenuOption; //only a bool because there are only 2 possible options

    int numberOfGoals = 5;

    public KeyCode Forward;
    public KeyCode Back;
    public KeyCode jumpKey;
    public KeyCode Left;
    public KeyCode Right;

    public GameObject play;
    public GameObject goalSettings;
    public GameObject scoreToWinObject; //the objetc that hold the number of goals after the scene switches

    public string sceneToLoad;

    string settingsString;
    // Use this for initialization
    void Start () {
        selectedMenuOption = true;
        play.GetComponent<MenuOptionScript>().selected = true;

        settingsString = goalSettings.GetComponent<Text>().text;
        goalSettings.GetComponent<Text>().text = settingsString + numberOfGoals;


    }
	
	// Update is called once per frame
	void Update () {

        MenuOptionScript playMenuScript = play.GetComponent<MenuOptionScript>();
        MenuOptionScript goalSettingsMenuScript = goalSettings.GetComponent<MenuOptionScript>();

        if (Input.GetKey(Forward)) //up
        {
            selectedMenuOption = true;

            playMenuScript.selected = true;
            goalSettingsMenuScript.selected = false;
        }

        if (Input.GetKey(Back)) //down
        {
            selectedMenuOption = false;

           playMenuScript.selected = false;
           goalSettingsMenuScript.selected = true;
        }
        
        if (Input.GetKeyUp(jumpKey) && selectedMenuOption /* if "play" option is selected, this is true*/) //select button
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

            goalSettings.GetComponent<Text>().text = settingsString + numberOfGoals;
        }


    }
}

