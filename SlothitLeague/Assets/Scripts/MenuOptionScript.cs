//currently not used in the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptionScript : MonoBehaviour {


    public bool selected = false;
    Color startingColor;
    bool scale = true;
    float f = 0;
    Text thisText;

    Vector3 startingScale;

	// Use this for initialization
	void Start () {
       thisText = this.GetComponent<Text>();
        startingColor = thisText.color;
        startingScale = thisText.gameObject.transform.localScale;
        
    }
	
	// Update is called once per frame
	void Update () {
		 
        if(selected)
        {
            thisText.color = Color.red;
            f += Time.deltaTime;
            if(f > 0.3)
            {
                f = 0;
                scale = !scale;
            }

            if(scale)
            {
                thisText.gameObject.transform.localScale = thisText.gameObject.transform.localScale + thisText.gameObject.transform.localScale * Time.deltaTime; 
                
            }
            else
            {
                thisText.gameObject.transform.localScale = thisText.gameObject.transform.localScale - thisText.gameObject.transform.localScale * Time.deltaTime;
            }


        }
        else
        {
            f = 0;
            scale = true;
            thisText.color = startingColor;
            thisText.gameObject.transform.localScale = startingScale;
        }

	}
}
