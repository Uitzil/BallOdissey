using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserShooter : MonoBehaviour
{
    public Camera playerCamera;
    public Transform laserOrigin;
    public float gunRange = 50f;
   
    public float laserDuration = 0.05f;

    public float waitDuration = 5f;

    LineRenderer laserLine;

    private Transform player;
    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();

        player = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        Vector3 playerPos = new Vector3(player.position.x, player.position.y, player.position.z);


        if (Vector3.Distance(transform.position, playerPos) > gunRange)
        { 


            laserLine.SetPosition(0, laserOrigin.position);
            Vector3 rayOrigin = new Vector3(0, 0, 0);
            //playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));


            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, transform.forward, out hit, gunRange))
            {
                laserLine.SetPosition(1, hit.point);
                
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (transform.forward * gunRange));
            }
            StartCoroutine(ShootLaser());
        }
    }

    IEnumerator ShootLaser()
    {
        
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;

    }
}