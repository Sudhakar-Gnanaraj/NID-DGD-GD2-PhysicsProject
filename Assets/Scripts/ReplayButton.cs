using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour
{
    SpriteRenderer sr;
    Color originalColor;
    [SerializeField] Color hoverColor = Color.green;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
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
                SceneManager.LoadScene("Level 2");
            }
        }
        else
        {
            sr.color = originalColor;
        }
    }
}
