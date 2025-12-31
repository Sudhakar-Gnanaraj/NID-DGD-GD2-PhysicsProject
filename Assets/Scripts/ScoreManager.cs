using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Details")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI scoreUpdateText;
    //[SerializeField] int pointsToAdd = 10;
    //[SerializeField] int pointsToRemove = 10;
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
        //scoreKeeper.ResetScore();
    }
    void Update()
    {
        scoreText.text = "Score: "+ points;
    }
    public void ChangeScore(bool value, int updatepoints)
    {
        if (value)
        {
            points += updatepoints;
            ScoreUpdate(value,updatepoints);
            scoreKeeper.AddScore(updatepoints);
        }
        else
        {
            points -= updatepoints;
            ScoreUpdate(value,updatepoints);
            scoreKeeper.ReduceScore(updatepoints);
        }
    }

    public void ScoreUpdate(bool value, int updatepoints)
    {
        if(updateCo != null)
            StopCoroutine(updateCo);
        
        StartCoroutine(ShowUpdate(value, updatepoints));
    }

    private IEnumerator ShowUpdate(bool colour, int updatepoints)
    {
        if (colour)
        {
            scoreUpdateText.color = Color.forestGreen;
            scoreUpdateText.text = "+" + updatepoints;
        }
        else
        {
            scoreUpdateText.color = Color.red;
            scoreUpdateText.text = "-" + updatepoints;
        }

        scoreUpdateText.enabled = true;
        yield return new WaitForSeconds(updateShowTime);
        scoreUpdateText.enabled = false;
    }
}
