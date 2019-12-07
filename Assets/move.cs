using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class move : MonoBehaviour {
    public int direction = 0;
    public double xpos = 5;
    public double ypos = 5;
    public int coin = 0;
    public float distToGround;
    public Rigidbody2D rd;
    int timer = 0;
    public Text textbox;
    public int time = 0;
    public GameObject coinobj;
    public Slider healthBar;
    public float gamespeed = 1;
    void takeDamage()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    void untakeDamage()
    {
        GetComponent<SpriteRenderer>().color = Color.white;

    }
    private void Start()    
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        makeCoin(35, 35);
    }
    void OnCollisionEnter2D(Collision2D theCollision)
    {
        Debug.Log(theCollision.gameObject.tag);
        
        if (theCollision.gameObject.tag == "Coin")
        {
            coin++;
            System.Random r = new System.Random();
            makeCoin((float)r.Next(10,90), (float)r.Next(10, 90));
        }
        else
        {
            Time.timeScale = 2;
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    void makeCoin(float col, float row)
    {
        Vector2 pos = new Vector2(col, row);
        Instantiate(coinobj, pos, Quaternion.Euler(new Vector3(0, 0, 0)));
    }

    void FixedUpdate() {
        textbox.text = "Coins: " + coin.ToString();
        gamespeed = (float)(Math.Pow((float)(1.01), (float)(coin)));

        if (Input.GetKey("a"))
        {
            direction = 0;
         }
        if (Input.GetKey("d"))
        {
            direction = 1;
        }
        if(Input.GetKey("w"))
        {
            direction = 2;
        }
        if (Input.GetKey("s"))
        {
            direction = 3;
        }
        if (Input.GetKey("q") && Time.timeScale == 2)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            coin = 0;
            Time.timeScale = 1;
            xpos = 5;
            ypos = 5;
            System.Random r = new System.Random();
        }
        if (Time.timeScale != 2)
        {
            if (direction == 0)
            {
                xpos = Math.Max(5, xpos - 1*gamespeed);
                if (xpos == 5)
                    direction = 1;
            }
            if (direction == 1)
            {
                xpos = Math.Min(85, xpos + 1* gamespeed);
                if (xpos == 85)
                    direction = 0;
            }
            if (direction == 2)
            {
                ypos = Math.Min(ypos + 1* gamespeed, 85);
                if (ypos == 85)
                    direction = 3;
            }
            if (direction == 3)
            {
                ypos = Math.Max(ypos - 1* gamespeed, 5);
                if (ypos == 5)
                    direction = 2;
            }
            transform.position = new Vector2((float)(xpos * 1), (float)(ypos * 1));
            time++;
            timer--;
            Debug.Log(time);
        }

    }
}
