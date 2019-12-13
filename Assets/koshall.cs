using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koshall : MonoBehaviour {
    public Rigidbody2D rd;
    public int timer = 300;
    public int intial = 0;
    // Use this for initialization
    void Start () {
        intial = timer;
	}
    void OnCollisionEnter2D(Collision2D theCollision)
    {

        Destroy(gameObject);
    }
    void Update () {
        timer--;
        if(!gameObject.name.Equals("Flare") && (intial - timer > 300 || Time.timeScale == 2))
        {
            Destroy(gameObject);
        }
    }
}
