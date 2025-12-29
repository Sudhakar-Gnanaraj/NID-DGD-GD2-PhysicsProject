using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour
{
    SpriteRenderer sr;
    Color originalColor;
    [SerializeField] Color hoverColor = Color.green;
    [SerializeField] string buttonDetail;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        originalColor = sr.color;
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePos);

        if (hit != null && hit.gameObject == gameObject)
        {
            sr.color = hoverColor;
            if (Input.GetMouseButtonDown(0))
            {
                scoreKeeper.ResetScore();
                if(buttonDetail == "Level 1")
                    SceneManager.LoadScene("Level 1");
                else if(buttonDetail == "Level 2")
                    SceneManager.LoadScene("Level 2");
                else if(buttonDetail == "Replay")
                    SceneManager.LoadScene(scoreKeeper.GetLevel());
                else if(buttonDetail == "Play")
                    SceneManager.LoadScene("LevelSelect");
                else if(buttonDetail == "Main")
                    SceneManager.LoadScene("MainMenu");
                else if(buttonDetail == "Exit")
                    Application.Quit();
            }
        }
        else
        {
            sr.color = originalColor;
        }
    }
}
