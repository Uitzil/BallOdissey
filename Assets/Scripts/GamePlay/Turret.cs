using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] float turretRange = 13f;
    [SerializeField] float turretRotationSpeed = 5f;

    private Transform player;


    void Start()
    {
        player = FindObjectOfType<Player>().transform;

    }

    void Update()
    {

        Vector3 playerPos = new Vector3(player.position.x, player.position.y, player.position.z);


        if(Vector3.Distance(transform.position,playerPos)> turretRange) { return;  }



        Vector3 playerDirection = playerPos - transform.position;

        float turretRotationStep = turretRotationSpeed * Time.deltaTime;

        Vector3 newLookDirection = Vector3.RotateTowards(transform.up, playerDirection, turretRotationStep, 0f);

        transform.rotation = Quaternion.LookRotation(newLookDirection);

    }
}