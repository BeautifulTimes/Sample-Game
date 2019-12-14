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
    public int lastCoin = 0;
    public float distToGround;
    public Rigidbody2D rd;
    public Text textbox;
    public int time = 0;
    public GameObject coinobj;
    private Vector3 fp;
    private Vector3 lp;
    private float dragDistance;  
    public Slider healthBar;
    public float gamespeed = 1;
    public Animator CoinAnimation;
    private void Start()    
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        makeCoin(35, 35);
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
    }
    void OnCollisionEnter2D(Collision2D theCollision)
    {
        
        if (theCollision.gameObject.tag == "Coin")
        {
            coin++;
            System.Random r = new System.Random();
            makeCoin(5 + (float)r.Next(0,9)*10, 5 + (float)r.Next(0, 9) * 10);
            CoinAnimation.SetBool("OnCoinTouch", true);
            lastCoin = time;
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
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list
                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            direction = 1;
                            Debug.Log("Right Swipe");
                        }
                        else
                        {   //Left swipe
                            direction = 0;
                            Debug.Log("Left Swipe");
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                                        direction = 2;
                            Debug.Log("Up Swipe");
                        }
                        else
                        {   //Down swipe
                            direction = 3;
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
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
            if(time - lastCoin > 40)
                CoinAnimation.SetBool("OnCoinTouch", false);
        }

    }
}
