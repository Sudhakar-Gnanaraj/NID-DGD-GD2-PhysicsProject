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
    void Update()
    {
        scoreText.text = scoreKeeper.GetScore();
    }

    
}
