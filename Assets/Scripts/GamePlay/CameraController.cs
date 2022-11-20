using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    
    private Vector3 cameraoffset;
    public Transform targetObject;
    

   



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

        
        Vector3 newPosition = targetObject.transform.position + cameraoffset;

        transform.position = newPosition;
    }
    void OnDisable()
    {
       // GameController.PSpawn -= ResetCameraSpawn;

    }
}
