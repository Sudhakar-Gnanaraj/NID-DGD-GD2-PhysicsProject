using System;
using Unity.VisualScripting;
using UnityEngine;

public class RandomForce : MonoBehaviour
{
    private Rigidbody2D rb;
    AudioPlayer audioPlayer;
    [SerializeField] float forceValue;
    //bool isPlayingAudio;
 
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioPlayer = FindAnyObjectByType<AudioPlayer>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            AddForce();
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioPlayer.PlayPaddleClip();
        }
    }
    void AddForce()
    {
        rb.linearVelocityX = forceValue;
    }
  

}
