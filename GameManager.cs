using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool gameOver = true;
    public GameObject player;

    private UI_Manager _uI_Manager;

    void Start()
    {
        _uI_Manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
    }
    
    // if gameover is true
    //if space key pressed
    //spawn player
    //game over = false
    // hide title screen

    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player, Vector3.zero, Quaternion.identity);
                gameOver = false;
                _uI_Manager.HideTitleScreen();

            }
        }
    }



}
