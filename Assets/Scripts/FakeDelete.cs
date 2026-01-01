using UnityEngine;

public class FakeDelete : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Instead of destroying, return object back to pool
        collision.gameObject.SetActive(false);
    }
}
