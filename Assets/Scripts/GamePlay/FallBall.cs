using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBall : MonoBehaviour
{

    public delegate void RAction();
    public static event RAction ReSpawn;
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            //if (ReSpawn != null) ReSpawn();
        }   
    }
}
