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
    GameObject preff;
    GameObject preff2;
    public GameObject coin;
    public int[] attacks = new int[20];
    public GameObject[] laserCols = new GameObject[9];
    public int[] laserColsTimer = new int[9];
    public int dash;
    int laser = 0;
    public Slider healthbar;
    public double time1 = 0;
    public Text temptext;
    public float projectilespeed = (float)1000;
    int direction = -1;
   public  int time = 0;
    public int health = 2000;
    int attk4 = -1;
    public float maxvel;
    public int lasertimer = -1;
    public int attcool = 0;
    public float gamespeed = 1;
    void fireLaser(int col)
    {
        laserColsTimer[col] = 150;
        Destroy(laserCols[col]);
        Vector2 pos = new Vector2((float)(rd.position.x + 10 * col + 5), (float)(rd.position.y));
        laserCols[col] = Instantiate(prelaser, pos, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
    void fireBullet2(double angle, int col)
    {
        if (col < 0)
            return;
        angle = angle * Math.PI / 180;
        Rigidbody2D projectileInstance;
        Vector2 pos = new Vector2((float)(rd.position.x -44), (float)(rd.position.y + Math.Sin(angle) * 14 + 10*col)+55);
        projectileInstance = Instantiate(flare, pos, Quaternion.Euler(new Vector3(0, 0, 0)));
        projectileInstance.AddForce(new Vector2((float)(0.25)* gamespeed, (float)(projectilespeed * Math.Sin(angle))));
    }
    void fireBullet(double angle, int col)
    {
        angle = angle * Math.PI / 180;
        Rigidbody2D projectileInstance;
      
        Vector2 pos = new Vector2((float)(rd.position.x +10*col+2 + 4), (float)(rd.position.y));
        projectileInstance = Instantiate(flare, pos, Quaternion.Euler(new Vector3(0, 0, 0)));
       projectileInstance.AddForce(new Vector2((float)(projectilespeed *Math.Cos(angle)), (float)(0.25* gamespeed)));
   
    }
    void attack(int num)
    {
        Debug.Log(num);
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
            for (int x = 1; x < 9; x +=2)
            {
                fireBullet2(0, x);
            }
        }
        if(num == 6)
        {
            attacks[5] = 100;
            fireBullet2(0, 0);
            fireBullet2(0, 8);
            fireBullet2(0, 4);
            fireBullet(90, 0);
            fireBullet(90, 8);
            fireBullet(90, 4);
        }
        if (num == 7)
            attacks[6] = 300;
        if(num == 9)
        {
            attacks[8] = 100;
            fireLaser(4);
            fireBullet(90, 0);
            fireBullet(90, 8);
        }
        if(num == 10)
        {
            fireLaser(0);
            fireLaser(3);
            fireLaser(4);
            fireLaser(8);
            attacks[9] = 100;
        }
        if(num == 11)
        {
            attacks[10] = 300;
        }
        if(num == 12)
        {
            attacks[11] = 500;
        }
        if(num == 13)
        {
            attacks[12] = 100;
            fireLaser(8);
            fireLaser(0);
            fireLaser(4);
        }
        if(num == 14)
        {
            attacks[13] = 100;
            fireLaser(4);
            fireBullet(90, 0);
            fireBullet(90, 1);
            fireBullet(90, 7);
            fireBullet(90, 8);
        }
        if (num == 15)
        {
            fireLaser(1);
            fireLaser(7);
            fireLaser(2);
        }
        if(num == 16)
        {
            attacks[15] = 100;
            for (int x = 1; x < 9; x += 2)
            {
                fireBullet2(180, x);
            }
        }
        if(num == 17)
        {
            attacks[16] = 200;
        }
        if (num == 20)
        {
            attacks[19] = 100;
        }
        Debug.Log(num);
    }
    void Start () {
        attacks = new int[20];
        for(int x = 0;x<9;x++)
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
                if (laserColsTimer[x] < 100)
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
            bool con22 = true;
            for (int x = 0; x < attacks.Length; x++)
            {
                if (attacks[x] > -150)
                    con22 = false;
            }
            if (con22)
            {
                System.Random r = new System.Random();
                int aa = 1;
                int bb = 1;
                bb = (int)r.Next(1, 17);
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
            if (attacks[6] >= 0 && attacks[6] % 60 == 0)
            {
                fireBullet(90, 8 - (attacks[6] / 60));
            }

            if (attacks[11] % 250 == 0 && attacks[11] > 0)
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
                fireBullet2(180, attacks[16] / 40);
            }
            if (attacks[10] % 50 == 0 && attacks[10] >= 0)
            {
                fireLaser(7 - (attacks[10] / 60));
            }
            lasertimer--;
            if (lasertimer == 0)
            {
                Destroy(preff);
                preff2 = Instantiate(laserobj, new Vector2(rd.position.x, rd.position.y + 4), Quaternion.Euler(new Vector3(0, 0, 1)));
            }
            if (lasertimer == -35)
            {
                Destroy(preff2);
            }
            attk4--;
        }
        else
        {
            time = 0;
            for(int x = 0;x<9;x++)
            {
                Destroy(laserCols[x]);
                laserColsTimer[x] = -300;
            }
        }
    }
}
