using TMPro;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
    }

    void Start()
    {
          
    }
    void Update()
    {
        if (scoreKeeper == null)
            scoreKeeper = FindAnyObjectByType<ScoreKeeper>();

        if (scoreKeeper != null)
            scoreText.text = scoreKeeper.GetScore();
    }

    
}
