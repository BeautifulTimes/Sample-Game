using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wizard : MonoBehaviour {
    public Text tex_box;
    public Text cointextbox;
    public int coins;
    public Rigidbody2D rd;
    public Rigidbody2D flare;
    public GameObject prelaser;
    public GameObject laserobj;
    public GameObject coin;
    public int[] attacks = new int[20];
    public GameObject[] laserCols = new GameObject[9];
    public int[] laserColsTimer = new int[9];
    public GameObject[] laserRows = new GameObject[9];
    public int[] laserRowsTimer = new int[9];
    public double time1 = 0;
    public Text temptext;
    public float projectilespeed = (float)1000;
    public int time = 0;
    public int lasertimer = -1;
    public float gamespeed = 1;
    public int direction = 1;
    void fireLaser(int col)
    {
        if (col < 0)
            return;
        if (direction % 2 == 0)
        {
            laserColsTimer[col] = 150;
            Destroy(laserCols[col]);
            Vector2 pos = new Vector2((float)(rd.position.x + 10 * col + 5), (float)(rd.position.y));
            laserCols[col] = Instantiate(prelaser, pos, Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        else
        {
            laserRowsTimer[col] = 150;
            Destroy(laserRows[col]);
            Vector2 pos = new Vector2((float)(rd.position.x), (float)(rd.position.y + 10 * col + 45));
            laserRows[col] = Instantiate(prelaser, pos, Quaternion.Euler(new Vector3(0, 0, 90)));
        }

    }

    void fireBullet(double angle, int col)
    {
        if (col < 0)
            return;
        if (direction % 2 == 0)
        {
            angle = angle * Math.PI / 180;
            Rigidbody2D projectileInstance;

            Vector2 pos = new Vector2((float)(rd.position.x + 10 * col + 6), (float)(rd.position.y));
            projectileInstance = Instantiate(flare, pos, Quaternion.Euler(new Vector3(0, 0, 0)));
            projectileInstance.AddForce(new Vector2((float)(0 ), (float)(0.25 * gamespeed)));
            gvars.bulletCols[col] = true;
        }
        else
        {

            angle = angle * Math.PI / 180;
            Rigidbody2D projectileInstance;
            Vector2 pos = new Vector2((float)(rd.position.x - 44), (float)(rd.position.y  + 10 * col) + 65);
            projectileInstance = Instantiate(flare, pos, Quaternion.Euler(new Vector3(0, 0, 0)));
            projectileInstance.AddForce(new Vector2((float)(0.25) * gamespeed, (float)(0)));
            gvars.bulletRows[col] = true;
        }
    }
    void attack(int num)
    {
        if (num == 1)
        {
          
            attacks[0] = 250;
        }
        if(num == 2)
        {
            attacks[1] = 200;
            for (int x = 1; x < 9; x += 2)
            {
                fireBullet(90, x);
            }
        }
        if(num == 3)
        {
            attacks[2] = 60;
        }
        if (num == 4)
            attacks[3] = 250;
        if(num == 5)
        {
            attacks[5] = 100;
            fireBullet(90, 0);
            fireBullet(90, 8);
            fireBullet(90, 4);
            direction++;
            fireBullet(90, 0);
            fireBullet(90, 8);
            fireBullet(90, 4);
        }
        if(num == 6)
        {
            fireLaser(0);
            fireLaser(3);
            fireLaser(4);
            fireLaser(5);
            fireLaser(8);
            attacks[9] = 100;
        }
        if(num == 7)
        {
            attacks[19] = 100;
            attacks[11] = 500;
        }
        if(num == 8)
        {
            attacks[12] = 100;
            fireLaser(8);
            fireLaser(0);
            fireLaser(4);
        }
        if(num == 9)
        {
            attacks[19] = 100;
            fireLaser(4);
            fireBullet(90, 0);
            fireBullet(90, 1);
            fireBullet(90, 7);
            fireBullet(90, 8);
        }
        if (num == 10)
        {
            attacks[19] = 100;

            fireBullet(90, 1);
            fireBullet(90, 2);
            fireBullet(90, 3);
            fireBullet(90, 5);
            fireBullet(90, 6);
            fireBullet(90, 7);


        }
        if(num == 11)
        {
            attacks[19] = 100;
            fireBullet(90, 3);
            fireBullet(90, 4);
            fireBullet(90, 5);
            direction++;

            fireBullet(90, 3);
            fireBullet(90, 4);
            fireBullet(90, 5);

        }
        if(num == 12)
        {
            attacks[19] = 100;
            fireBullet(90, 0);
            fireBullet(90, 1);
            fireBullet(90, 2);
            fireBullet(90, 6);
            fireBullet(90, 7);
            fireBullet(90, 8);
            direction++;
            fireBullet(90, 0);
            fireBullet(90, 1);
            fireBullet(90, 2);
            fireBullet(90, 6);
            fireBullet(90, 7);
            fireBullet(90, 8);
        }
    }
    void Start () {
        attacks = new int[20];
        for (int x = 0;x<9;x++)
        {
            laserColsTimer[x] = -1;
        }
        for (int x = 0;x<attacks.Length;x++)
        {
            attacks[x] = -1;

        }
    }
	void Update () {
        
        if (Time.timeScale != 2)
        {
            gamespeed = (float)(Math.Pow((float)(1.01), (float)(coins)));
            coins = int.Parse(cointextbox.text.Substring(6));
            tex_box.text = "Score: " + time.ToString();
            time++;
            for (int x = 0; x < attacks.Length; x++)
            {
                attacks[x]--;
            }
            for (int x = 0; x < 9; x++)
            {
                laserColsTimer[x]--;
                if (laserColsTimer[x] < 30)
                {
                    Destroy(laserCols[x]);
                    Vector2 pos = new Vector2((float)(rd.position.x + 10 * x + 5), (float)(rd.position.y));
                    laserCols[x] = Instantiate(laserobj, pos, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
                if (laserColsTimer[x] < 0)
                {
                    Destroy(laserCols[x]);
                }
            }
            for (int x = 0; x < 9; x++)
            {
                laserRowsTimer[x]--;
                if (laserRowsTimer[x] < 30)
                {
                    Destroy(laserRows[x]);
                    Vector2 pos = new Vector2((float)(rd.position.x ), (float)(rd.position.y + 10 * x + 45));
                    laserRows[x] = Instantiate(laserobj, pos, Quaternion.Euler(new Vector3(0, 0, 90)));
                }
                if (laserRowsTimer[x] < 0)
                {
                    Destroy(laserRows[x]);
                }
            }
            bool con22 = true;
            for (int x = 0; x < attacks.Length; x++)
            {
                if (attacks[x] > -150)
                    con22 = false;
            }
            if (con22)
            {
                for (int x = 0; x < 9; x++)
                {
                    gvars.bulletRows[x] = false;
                    gvars.bulletCols[x] = false;
                }
                System.Random r = new System.Random();
                int aa = 1;
                int bb = 1;
                bb = (int)r.Next(1, 13);
                aa = (int)r.Next(0, 2);
                direction += aa;
                Debug.Log(aa);
                attack(bb);
            }
            if (attacks[0] >= 0 && attacks[0] % 50 == 0)
            {
                fireBullet(90, attacks[0] / 50);
                fireBullet(90, 8 - (attacks[0] / 50));

            }
            if (attacks[1] == 0)
            {
                for (int x = 0; x < 9; x += 2)
                {
                    fireBullet(90, x);
                }
            }
            if (attacks[2] == 0)
            {
                for (int x = 1; x < 7; x++)
                    fireBullet(90, x);
            }
            if (attacks[3] >= 0 && attacks[3] % 50 == 0)
            {
                fireBullet(90, (250 - attacks[3]) / 50);
                fireBullet(90, 8 - ((250 - attacks[3]) / 50));

            }
            if (attacks[11] % 120 == 0 && attacks[11] > 0)
            {
                System.Random r = new System.Random();
                int bb = (int)r.Next(1, 8);
                fireLaser(bb);

            }

            if (attacks[7] == 0)
            {
                for (int x = 8; x > 2; x++)
                    fireBullet(90, x);
            }
            if (attacks[16] % 40 == 0)
            {
                fireBullet(180, attacks[16] / 40);
            }
        }
        else
        {
            time = 0;
            for(int x = 0;x<9;x++)
            {
                Destroy(laserCols[x]);
                laserColsTimer[x] = -300;
            }
            for (int x = 0; x < 9; x++)
            {
                Destroy(laserRows[x]);
                laserRowsTimer[x] = -300;
            }
        }
    }
}
