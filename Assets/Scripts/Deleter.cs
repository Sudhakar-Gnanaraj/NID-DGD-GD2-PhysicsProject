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
        // Disable object instead of destroying
        collision.gameObject.SetActive(false);

        // If it's a collectible object, update the level counts
        if (collision.CompareTag("Potato") || collision.CompareTag("Onion"))
        {
            levelManager.UpdateTotalObjects();
        }
    }
}
