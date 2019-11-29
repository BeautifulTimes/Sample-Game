using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class FlyingEnemy  : MonoBehaviour
{
    public Rigidbody2D rd;
    public Rigidbody2D player;

    public Rigidbody2D projectile;
    public Animator animator;

    public float projectilespeed = (float)0.05;
    public int maxvel = 10;
    public int maxjump = 2000;
    public int health = 60;
    int curmove = 0;
    int time = 0;
    int attcool;
   // public int updatemove = 200;
    bool rightfacing = true;
    bool isgrounded = true;

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

    void OnCollisionEnter2D(Collision2D theCollision)
    {

   
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

     void fireBullet()
    {
        Rigidbody2D projectileInstance;
        if (rd.position.x > player.position.x)
        {
            Vector2 pos = new Vector2(rd.position.x - 5,(float)( rd.position.y-0.3));
            projectileInstance = Instantiate(projectile,pos, Quaternion.Euler(new Vector3(0, 0, 1))) as Rigidbody2D;
            projectileInstance.AddForce(new Vector2(-(float)(projectilespeed), 0));
        }
        else
        {
            Vector2 pos = new Vector2(rd.position.x + 8 , (float)(rd.position.y-0.3));
            projectileInstance = Instantiate(projectile, pos, Quaternion.Euler(new Vector3(0, 0, 1))) as Rigidbody2D;
            projectileInstance.AddForce(new Vector2((float)(projectilespeed), 0));
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
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();

    }


    // Update is called once per frame
    void Update()
    {
        System.Random random = new System.Random();
        attcool--;
        if(attcool == 0)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        SaneVelocity();
        time++;
        if(time % 100 == 50)
            animator.SetBool("Attack", true);
        if (time % 100 == 0)
        {
            fireBullet();
            animator.SetBool("Attack", false);
                
        }
  
        if (time % 120 == 0)
        {
            if (random.Next(0, 100) < 50)
            {
                curmove = 0;
            }
            else
            {
                curmove = 1;

            }
            if (random.Next(0, 100) < 1 && isgrounded)
            {
                curmove = 2;
                isgrounded = false;
            }

        }
       /* if(curmove == 0)
        {
            rd.AddForce(new Vector2(maxvel, 0));
        }
        if (curmove == 1)
        {
            rd.AddForce(new Vector2(-maxvel, 0));
        }
         if(curmove == 2 && isgrounded)
        {
            rd.AddForce(new Vector2(0, maxjump));

        }*/


    }
}



