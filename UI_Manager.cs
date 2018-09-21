using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {

    public Sprite[] lives;
    public Image livesImageDisplay;
    public GameObject titleScreen;
    public Text scoreText;
    public int score;



 

	public void UpdateLives(int currentLives)
    {
        Debug.Log("PLayer lives  " + currentLives);
       livesImageDisplay.sprite =  lives[currentLives];
    }

    public void UpdateScore(int addedScore)
    {
        score += addedScore;
        scoreText.text = "Score: " + score;
       
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        scoreText.text = "Score: ";
    }
}
