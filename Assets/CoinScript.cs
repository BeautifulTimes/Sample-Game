using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public Rigidbody2D rd;
    public int timer = 300;
    public int intial = 0;
    // Use this for initialization
    void Start()
    {
        intial = timer;
    }
    void OnCollisionEnter2D(Collision2D theCollision)
    {
        //   if(theCollision.gameObject.tag == "Arrow")

        Destroy(gameObject);
        /*(theCollision.gameObject.tag == "Arrow")
        {
            Debug.Log("hi");
            Physics2D.IgnoreCollision(theCollision.gameObject.GetComponent<Collider2D>(), rd.gameObject.GetComponent<Collider2D>());
        }*/
    }
    void Update()
    {

    }
}
