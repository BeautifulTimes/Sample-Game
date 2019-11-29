using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sideways : MonoBehaviour
{
    public Rigidbody2D rd;
    public Rigidbody2D flare;
    public GameObject prelaser;
    public GameObject laserobj;
    GameObject preff;
    GameObject preff2;

    public int dash;
    int laser = 0;
    public Slider healthbar;
    public double time1 = 0;
    public Text temptext;
    public float projectilespeed = (float)0.05;
    int direction = -1;
    public int time = 0;
    public int health = 2000;
    int attk4 = -1;
    public float maxvel;
    public int lasertimer = -1;

    public int attcool = 0;

    public void takeDamage()
    {
        Debug.Log("Wizard damage");
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
    public void move(int loc)
    {
        if (loc == 1)
        {
            rd.position = new Vector2((float)323.5, (float)(5.500004));
        }
        else if (loc == 2)
        {
            rd.position = new Vector2((float)300, (float)(21.6));
        }
        else if (loc == 3)
        {
            rd.position = new Vector2((float)353.5, (float)(21.6));

        }
        else if (loc == 4)
        {
            rd.position = new Vector2((float)324.2, (float)(35.2));

        }
    }
    void fireBullet(double angle)
    {
        angle = angle * Math.PI / 180;
        Rigidbody2D projectileInstance;
        if (angle == Math.PI / 180 * 180)
        {
            Debug.Log(Math.Sin(angle) * 7);
        }
        Vector2 pos = new Vector2((float)(rd.position.x + Math.Cos(angle) * 14), (float)(rd.position.y + Math.Sin(angle) * 14));
        projectileInstance = Instantiate(flare, pos, Quaternion.Euler(new Vector3(0, 0, 1)));
        projectileInstance.AddForce(new Vector2((float)(projectilespeed * Math.Cos(angle)), (float)(projectilespeed * Math.Sin(angle))));

    }

    void attack(int num)
    {
        if (num == 2)
            fireBullet(0);
        if (num == 3)
            fireBullet(0);


    }
    void Start()
    {
        //attack(3);
    }
    void Update()
    {
        time++;
        time1 += Time.deltaTime;
        if (time % 100 == 0)
        {
            System.Random r = new System.Random();
            int aa = 1;
            int bb = 1;
            bb = (int)r.Next(1, 10);
            attack(bb);
            if (bb == 1 && lasertimer < -100)
            {
                lasertimer = 100;
                preff = Instantiate(prelaser, new Vector2(rd.position.x, rd.position.y + 4), Quaternion.Euler(new Vector3(0, 0, 1)));
            }
        }
        lasertimer--;
        if (lasertimer == 0)
        {
            Destroy(preff);
            preff2 = Instantiate(laserobj, new Vector2(rd.position.x, rd.position.y + 4), Quaternion.Euler(new Vector3(0, 0, 1)));
        }
        if (lasertimer == -50)
        {
            Destroy(preff2);
        }

        attk4--;

    }
}
