using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Slime : MonoBehaviour {
    public Rigidbody2D rd;
    public int maxvel = 10;
    bool rightfacing = true;
    bool isgrounded = true;

    void OnCollisionEnter2D(Collision2D theCollision)
    {

        if (theCollision.gameObject.CompareTag("Ground"))
        {
            isgrounded = true;
            //   Debug.Log("hi");

        }
        if (theCollision.gameObject.tag == "Attacking")
        {
            Destroy(gameObject);
            Debug.Log("you should be ded");
        }
    }
    void OnCollisionExit2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.tag == "Ground")
        {
            isgrounded = false;
        }
    }
    void SaneVelocity()
    {
    rd.angularVelocity = 0;

        if (rd.velocity.x < 0)
        {
            rd.velocity = new Vector2(Math.Min(rd.velocity.x + 1, 0), rd.velocity.y);

            if (rightfacing)
            {
                rightfacing = false;
                Vector3 hiderek = transform.localScale;
                hiderek.x = -hiderek.x;
                transform.localScale = hiderek;
            }
        }
        else
        {
            rd.velocity = new Vector2(Math.Max(rd.velocity.x - 1, 0), rd.velocity.y);
            if (!rightfacing)
            {
                rightfacing = true;
                Vector3 hiderek = transform.localScale;
                hiderek.x = -hiderek.x;
                transform.localScale = hiderek;
            }
        }

        if (rd.velocity.x > maxvel)
        {
            rd.velocity = new Vector2(maxvel, rd.velocity.y);
        }
        if (rd.velocity.x < -maxvel)
        {
            rd.velocity = new Vector2(-maxvel, rd.velocity.y);
        }
    }
    // Use this for initialization
    void Start () {
         rd = GetComponent<Rigidbody2D>();

    }


    // Update is called once per frame
    void Update () {
        System.Random random = new System.Random();

        SaneVelocity();
       if(random.Next(0,100) < 50)
        {
            rd.AddForce(new Vector2(700, 0));
        }
       else
        {
            rd.AddForce(new Vector2(-700, 0));

        }
       if(random.Next(0,100) < 1 && isgrounded)
        {
            rd.AddForce(new Vector2(0, 2000));
            isgrounded = false;
        }
    }
}
