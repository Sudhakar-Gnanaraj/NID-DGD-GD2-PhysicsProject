using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    ScoreManager scoreManager;

    void Awake()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Potato"))
            scoreManager.ChangeScore(true);
        else if(collision.CompareTag("Rock"))
            scoreManager.ChangeScore(false);
    }
}
