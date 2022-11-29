using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserShooter : MonoBehaviour
{

    public GameObject laserShooter;
    private Transform player;
    public float turretRange;
    public float waitTime=5f;

    private LineRenderer lr;

    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Vector3 playerPos = new Vector3(player.position.x, player.position.y, player.position.z);

        if (Vector3.Distance(transform.position, playerPos) > turretRange) {

          StartCoroutine  (Shoot());
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(waitTime);


        RaycastHit hit;
        //Debug.Log("playerdetected");


        if(Physics.Raycast(laserShooter.transform.position,laserShooter.transform.up,out hit))
        {
            
            if (hit.collider)
            {
                Debug.Log("shotting");

                lr.SetPosition(1, new Vector3(0, 0, hit.distance));

                 if (hit.collider.tag == "Player")
                 {

                     Debug.Log("die");
                 }
            }
            else
            {
                lr.SetPosition(1, new Vector3(0, 0, 5000));
            }
        }

    }
}