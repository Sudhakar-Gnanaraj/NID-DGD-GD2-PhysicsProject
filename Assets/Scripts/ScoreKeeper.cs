using UnityEngine;
using UnityEngine.Rendering;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper Instance;

    private int score = 0;

    void Awake()
    {
        // If an instance already exists AND it's not this → destroy duplicate
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set instance
        Instance = this;

        // Make it persistent across scenes
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int update)
    {
        score += update;
    }

    public void ReduceScore(int update)
    {
        score -= update;
    }

    public void ResetScore()
    {
        score  = 0;
    }

    public string GetScore()
    {
        return score.ToString();
    }

}
