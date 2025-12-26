using System;
using Unity.VisualScripting;
using UnityEngine;

public class RandomForce : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float forceValue;
 
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            AddForce();
        }
    }
    void AddForce()
    {
        rb.linearVelocityX = forceValue;
    }

}
