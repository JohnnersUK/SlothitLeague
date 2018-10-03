using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptionScript : MonoBehaviour {


    public bool selected = false;

    Text thisText;
	// Use this for initialization
	void Start () {
       thisText = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		 
        if(selected)
        {
            thisText.color = Color.red;
        }
        else
        {
            thisText.color = Color.black;
        }

	}
}
