using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreToWin : MonoBehaviour {

    public int score_to_win;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
