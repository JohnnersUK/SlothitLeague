using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostUI : MonoBehaviour {

    public GameObject player;
    Control c_script;

    GameObject b1;
    GameObject b2;
    GameObject b3;

    // Use this for initialization
    void Start ()
    {
        c_script = player.GetComponent<Control>();

        b1 = this.transform.GetChild(0).gameObject;
        b2 = this.transform.GetChild(1).gameObject;
        b3 = this.transform.GetChild(2).gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        int x = c_script.noBoosts;

        switch (x)
        {
            case 0:
                b1.GetComponent<Image>().color = new Color(0, 0, 0);
                b2.GetComponent<Image>().color = new Color(0, 0, 0);
                b3.GetComponent<Image>().color = new Color(0, 0, 0);
                break;
            case 1:
                b1.GetComponent<Image>().color = new Color(255, 255, 255);
                b2.GetComponent<Image>().color = new Color(0, 0, 0);
                b3.GetComponent<Image>().color = new Color(0, 0, 0);
                break;
            case 2:
                b1.GetComponent<Image>().color = new Color(255, 255, 255);
                b2.GetComponent<Image>().color = new Color(255, 255, 255);
                b3.GetComponent<Image>().color = new Color(0, 0, 0);
                break;
            case 3:
                b1.GetComponent<Image>().color = new Color(255, 255, 255);
                b2.GetComponent<Image>().color = new Color(255, 255, 255);
                b3.GetComponent<Image>().color = new Color(255, 255, 255);
                break;
        }
	}
}
