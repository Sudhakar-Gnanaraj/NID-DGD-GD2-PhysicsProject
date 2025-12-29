using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "LevelSelect" || SceneManager.GetActiveScene().name == "EndScreen" || SceneManager.GetActiveScene().name == "MainMenu")
            Cursor.visible = true;
        else
            Cursor.visible = false;
    }
}
