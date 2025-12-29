using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper Instance;

    private static int score = 0;
    private static int level = 1;

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

    public void UpdateLevel(int l)
    {
        level = l;
    }

    public int GetLevel()
    {
        return level;
    }

}
