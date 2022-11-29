using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    public Image lifeCount;
    public Sprite sixLife;
    public Sprite fiveLife;
    public Sprite fourLife;
    public Sprite threeLife;
    public Sprite twoLife;
    public Sprite oneLife;

    private float playerLives;
    public void Start()
    {
        Player.DiePlayer += ChangeLives;

        playerLives = FindObjectOfType<Player>().Lifes;
    }


    public void ChangeLives()
    {
        if (playerLives == 6)
        {
            lifeCount.sprite = sixLife;
        }
        if (playerLives == 5)
        {
            lifeCount.sprite = fiveLife;
        }
        if (playerLives == 4)
        {
            lifeCount.sprite = fourLife;
        }
        if (playerLives == 3)
        {
            lifeCount.sprite = threeLife;
        }
        if (playerLives == 2)
        {
            lifeCount.sprite = twoLife;
        }
        if (playerLives == 1)
        {
            lifeCount.sprite = oneLife;
        }

    }
}
