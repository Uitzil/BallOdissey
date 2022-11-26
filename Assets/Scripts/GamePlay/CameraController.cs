using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    
    private Vector3 cameraoffset;
    public Transform targetObject;
    public float smoothFactor = 0.5f;

   



    void Start()
    {

        cameraoffset = transform.position - targetObject.transform.position;

    }

    void OnEnable() {
       // GameController.PSpawn += ResetCameraSpawn;
    }



   

    /*void ResetCameraSpawn()
    {
        
        Vector3 FirstPosition = spawnCameraPoint.transform.position;
        transform.position = FirstPosition;
        

    }*/

    

    void LateUpdate() {

        transform.position= Vector3.Slerp(transform.position,targetObject.transform.position,smoothFactor);
        Vector3 newPosition = targetObject.transform.position + cameraoffset;

        transform.position = newPosition;
    }
    void OnDisable()
    {
       // GameController.PSpawn -= ResetCameraSpawn;

    }
}
