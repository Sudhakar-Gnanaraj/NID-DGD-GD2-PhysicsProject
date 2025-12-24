using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] GameObject playerObject;

    bool isPlayerDead;

    void Awake()
    {
        isPlayerDead = false;
    }

    public void killPlayer()
    {
        Debug.Log("Player Dead");
        Destroy(playerObject);
    }

    public void PlayerWin()
    {
        if(!isPlayerDead)
            Debug.Log("You Won!");
    }
}
