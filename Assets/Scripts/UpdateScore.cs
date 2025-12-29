using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    ScoreManager scoreManager;
    AudioPlayer audioPlayer;
    LevelManager levelManager;
    [SerializeField] string compareTag;

    void Awake()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
        audioPlayer = FindAnyObjectByType<AudioPlayer>();
        levelManager = FindAnyObjectByType<LevelManager>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(compareTag))
        {
            scoreManager.ChangeScore(true);
            audioPlayer.PlayCorrectClip();
            levelManager.UpdateTotalObjects();
        }
        else
        {
            scoreManager.ChangeScore(false);
            audioPlayer.PlayWrongClip();
            if(collision.CompareTag("Potato") || collision.CompareTag("Onion"))
                levelManager.UpdateTotalObjects();
        }
    
    }
}
