using UnityEngine;

public class Deleter : MonoBehaviour
{
    LevelManager levelManager;

    void Awake()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        if(collision.CompareTag("Potato") || collision.CompareTag("Onion"))
            levelManager.UpdateTotalObjects();
    }
}
