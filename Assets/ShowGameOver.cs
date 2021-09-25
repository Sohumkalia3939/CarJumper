using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGameOver : MonoBehaviour
{
     public GameObject GameOver;


public void gameover()
    {
        GameOver.SetActive(true);
    }
}
