using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserShooter : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] private Transform laserOrigin;
    public float waitTime;
    public float shootTime;
    public ParticleSystem sparks;
    public float gunRange = 5f;
    

    public delegate void shotAction();
    public static event shotAction gotShot;
    private Transform player;


   
      


            private void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;

         
}


    private void Update()
    {
        lr.SetPosition(0, laserOrigin.position);
        player = FindObjectOfType<Player>().transform;

        Vector3 playerPos = new Vector3(player.position.x, player.position.y, player.position.z);


        while (Vector3.Distance(transform.position, playerPos) > gunRange)
        {
            shoot();
            // StartCoroutine(waitShoot());
        }




        /* if(fireCountDown>= 0f)
         {
             shoot();

         }
         fireCountDown -= Time.deltaTime;*/

        //    StartCoroutine(waitShoot());
        //}

        //        IEnumerator waitShoot(){


        //         yield return new WaitForSeconds(waitTime*Time.deltaTime);
        //          lr.enabled = true;
        //         shoot();

        //        yield return new WaitForSeconds(shootTime*Time.deltaTime);

        //}
    }
    private void shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.up, out hit))
        {


            if (hit.collider)
            {
                sparks.Play();
                sparks.transform.position = hit.transform.position;
                lr.SetPosition(1, hit.point);

            }
            if (hit.transform.CompareTag("Player"))
            {

                if (gotShot != null)
                    gotShot();
            }
            else
            {
                lr.SetPosition(1, transform.up * 3000);
                sparks.Stop();
            }


        }
    }
    }

