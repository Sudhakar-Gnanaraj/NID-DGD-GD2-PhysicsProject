using UnityEngine;

public class LimitRotationSpeed : MonoBehaviour
{
    public float maxAngularSpeed = 50f; 
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxAngularSpeed, maxAngularSpeed);
    }
}
