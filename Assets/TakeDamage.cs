using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour {
    public int health = 100;
    public bool iscollide = false;
    void OnCollisionEnter2D(Collision2D theCollision)
    {
        Debug.Log(theCollision.gameObject.tag);
        if (theCollision.gameObject.tag == "Attacking")
        {
            iscollide = true;
          //  Destroy(gameObject);
        }
    }
    void OnCollisionExit2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.tag == "Attacking" || theCollision.gameObject.tag == "Player")
        {
            iscollide = false;

           // Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (iscollide)
            health -= 10;
        if (health <= 0)
            Destroy(gameObject);
	}
}
