using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordscript : MonoBehaviour {
    public int time = 0;
    void onCollisionEnter() { Destroy(gameObject); }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time++;
        if(time > 100)
           gameObject.tag = "Untagged";
    }
}
