using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public delegate void RAction();
    public static event RAction ReSpawn;
    
    
    
    public Transform spawnPoint;

    void Start()
    {

    }

   void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(ReSpawn!=null)
                ReSpawn();

           

        } 
    }

  
}
    