using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public delegate void RAction();
    public static event RAction ReSpawn;
    
    
    
    public Transform spawnPoint;

    void Start()
    {
        Player.GameOver += gameOverMetod;
    }

   void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(ReSpawn!=null)
                ReSpawn();

           

        } 
    }
    public void gameOverMetod()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }


}
    