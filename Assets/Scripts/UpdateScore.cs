using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    ScoreManager scoreManager;
    AudioPlayer audioPlayer;
    LevelManager levelManager;

    void Awake()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
        audioPlayer = FindAnyObjectByType<AudioPlayer>();
        levelManager = FindAnyObjectByType<LevelManager>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Potato"))
        {
            scoreManager.ChangeScore(true);
            audioPlayer.PlayCorrectClip();
            levelManager.UpdateTotalObjects();
        }
        else if (collision.CompareTag("Rock"))
        {
            scoreManager.ChangeScore(false);
            audioPlayer.PlayWrongClip();
        }
    
    }
}
