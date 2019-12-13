using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowBorder : MonoBehaviour {
    public Animator boAnimation;
    public int number;
    public int timer = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gvars.bulletCols[number])
        {
            timer++;
        }
        else
            timer = 0;
        if (timer > 60)
            gvars.bulletCols[number] = false;
        boAnimation.SetBool("Incoming", gvars.bulletCols[number]);
	}
}
