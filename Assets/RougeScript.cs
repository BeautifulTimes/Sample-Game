using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RougeScript : MonoBehaviour {
    int time = 0;
    public int health = 80;

    public Rigidbody2D rd;
    int direction = -1;
    public int attcool = 0;
    public int maxvel;
    Vector2 right;
    Vector2 left;
   public void takeDamage()
    {
        attcool = 30;
        health -= 40;
        GetComponent<SpriteRenderer>().color = Color.red;
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }

    void untakeDamage()
    {
        GetComponent<SpriteRenderer>().color = Color.white;

    }

    // Use this for initialization
    void Start () {
         right = new Vector2(maxvel, 0);
        left = new Vector2(-1 * maxvel, 0);
    }

    // Update is called once per frame
    void Update () {
        time++;
        attcool--;
        if (attcool == 15)
            untakeDamage();
        if (time % 300 == 1)
        {
            direction = direction * -1;
           
        }
        if (direction == 1)
        {
            rd.velocity = new Vector2(maxvel,rd.velocity.y);
        }
        else
            rd.velocity = new Vector2(-1*maxvel, rd.velocity.y);
    }
}
