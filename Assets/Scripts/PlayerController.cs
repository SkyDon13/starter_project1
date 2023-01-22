using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb2d;

    public TMP_Text countText;

    public TMP_Text GameOverText;
    public TMP_Text startText;
    private int count;

    bool gameOver;
    private Timer timer;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip pickupsSound;
    public AudioClip starterSound;
    AudioSource audioSource;

    void Start()
    {
        startText.text = "WASD to move! Collect 10 Pickups in 10 seconds!";
        rb2d = GetComponent<Rigidbody2D>();

        count = 0;

        GameOverText.text = "";

        SetCountText();

        audioSource = GetComponent<AudioSource>();

        GameObject timerObject = GameObject.FindWithTag("Timer");

        if (timerObject != null)
        {
            timer = timerObject.GetComponent<Timer>();

            print("Found the Timer Script");
        }

        if (timerObject == null)
        {
            print("Cannont Find Timer Script");


        }

        audioSource.PlayOneShot(starterSound);
    }
    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");


        float moveVertical = Input.GetAxis("Vertical");


        Vector2 movement = new Vector2(moveHorizontal, moveVertical);


        rb2d.AddForce(movement * speed);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickups"))

            other.gameObject.SetActive(false);

        count = count + 1;

        SetCountText();

        PlaySound(pickupsSound);
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    void Update()
    {
      
    }
   
    void SetCountText ()
    {
        countText.text = "Collected Pickups: " + count.ToString ();
        
        if (count >= 1)
        {
            Destroy(startText);
        }

        if (count >= 10)
        {
            Destroy(timer);
            gameOver = true;
            GameOverText.text = "You Win! Game by Skyler Donovan.";
            audioSource.PlayOneShot(winSound);
        }
    }
}
