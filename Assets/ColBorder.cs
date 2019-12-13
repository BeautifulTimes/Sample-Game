using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColBorder : MonoBehaviour
{
    public Animator boAnimation;
    public int number;
    public int timer = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gvars.bulletRows[number])
        {
            timer++;
        }
        else
            timer = 0;
        if (timer > 40)
            gvars.bulletRows[number] = false;
        boAnimation.SetBool("Incoming", gvars.bulletRows[number]);
    }
}
