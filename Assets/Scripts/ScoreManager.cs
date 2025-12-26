using System.Collections;
using TMPro;
using UnityEngine;

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

    void Start()
    {
        scoreUpdateText.enabled = false;
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
        }
        else
        {
            points -= pointsToRemove;
            ScoreUpdate(value);
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
            scoreUpdateText.color = Color.green;
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
