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
            scoreManager.ChangeScore(true,10);
            audioPlayer.PlayCorrectClip();
            levelManager.UpdateTotalObjects();
        }
        else if (collision.CompareTag("Rock"))
        {
            scoreManager.ChangeScore(false,10);
            audioPlayer.PlayWrongClip();
            if(collision.CompareTag("Potato") || collision.CompareTag("Onion"))
                levelManager.UpdateTotalObjects();
        }
        else
        {
            scoreManager.ChangeScore(true,2);
            audioPlayer.PlayWrongClip();
            if(collision.CompareTag("Potato") || collision.CompareTag("Onion"))
                levelManager.UpdateTotalObjects();
        }
    
    }
}
