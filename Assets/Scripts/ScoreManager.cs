using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Details")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI scoreUpdateText;
    [SerializeField] int pointsToAdd = 10;
    [SerializeField] int pointsToRemove = 10;
    [SerializeField] float updateShowTime = 2f;
    int points = 0;
    Coroutine updateCo;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
    }

    void Start()
    {
        scoreUpdateText.enabled = false;
        scoreKeeper.ResetScore();
    }
    void Update()
    {
        scoreText.text = "Score: "+ points;
    }
    public void ChangeScore(bool value)
    {
        if (value)
        {
            points += pointsToAdd;
            ScoreUpdate(value);
            scoreKeeper.AddScore(pointsToAdd);
        }
        else
        {
            points -= pointsToRemove;
            ScoreUpdate(value);
            scoreKeeper.ReduceScore(pointsToRemove);
        }
    }

    public void ScoreUpdate(bool value)
    {
        if(updateCo != null)
            StopCoroutine(updateCo);
        
        StartCoroutine(ShowUpdate(value));
    }

    private IEnumerator ShowUpdate(bool colour)
    {
        if (colour)
        {
            scoreUpdateText.color = Color.forestGreen;
            scoreUpdateText.text = "+" + pointsToAdd;
        }
        else
        {
            scoreUpdateText.color = Color.red;
            scoreUpdateText.text = "-" + pointsToRemove;
        }

        scoreUpdateText.enabled = true;
        yield return new WaitForSeconds(updateShowTime);
        scoreUpdateText.enabled = false;
    }
}
