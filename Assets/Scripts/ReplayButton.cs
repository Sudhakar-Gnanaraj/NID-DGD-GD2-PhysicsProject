using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour
{
    SpriteRenderer sr;
    Color originalColor;
    [SerializeField] Color hoverColor = Color.green;
    [SerializeField] string buttonDetail;
    ScoreKeeper scoreKeeper;
    AudioPlayer audioPlayer;
    Coroutine levelCo;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        audioPlayer = FindAnyObjectByType<AudioPlayer>();
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
                audioPlayer.PlayClickClip();
                

                if (levelCo != null)
                    StopCoroutine(levelCo);

                levelCo = StartCoroutine(LevelSwitch());
            }
        }
        else
        {
            sr.color = originalColor;
        }
    }

    IEnumerator LevelSwitch()
    {
        yield return new WaitForSeconds(0.2f);
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
