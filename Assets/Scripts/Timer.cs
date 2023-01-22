using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public TMP_Text timerText;
    public TMP_Text gameOverText;
    bool gameOver;

    public bool timerIsRunning = false;

    private PlayerController playerController;
    public AudioClip loseSound;
    public AudioClip backgroundSound;
    AudioSource audioSource;

    private void Start()
    {
        timerIsRunning = true;

        GameObject playerControllerObject = GameObject.FindWithTag("PlayerController");

        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();

            print("Found the PlayerController Script");
        } 

        if (playerController == null)
        {
            print("Cannont Find GameController Script");

           
        }
        audioSource = GetComponent<AudioSource>();
        gameOverText.text = "";

        audioSource.PlayOneShot(backgroundSound);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }


    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                Destroy(playerController);
                gameOver = true;
                gameOverText.text = "You lose Press R to restart!";
                audioSource.Stop();
                audioSource.PlayOneShot(loseSound);

               

            }


        }

        if (Input.GetKey(KeyCode.R))
        {
            if (gameOver == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        DisplayTime(timeRemaining);

        
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
