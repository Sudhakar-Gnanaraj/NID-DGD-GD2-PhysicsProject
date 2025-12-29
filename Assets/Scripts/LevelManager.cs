using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    [SerializeField] int potatoCount = 16;
    //[SerializeField] int onionCount = 0;
    //[SerializeField] string currentLevel;
    ScoreKeeper scoreKeeper;

    int potattoesSpawned = 0;
    int totalObjects = 0;
    Coroutine endScreenCo;
    void Awake()
    {
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
    }

    void Start()
    {
        //Cursor.visible = false;
        scoreKeeper.UpdateLevel(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadEndScreen()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("EndScreen");
    }

    public void updatePotatoCount()
    {
        potattoesSpawned++;
        
    }

    public int getPotatoCount()
    {
        return potatoCount;
    }
    public bool hasFinishedSpawn()
    {
        return (potatoCount == potattoesSpawned)? true: false;
    }

    public void UpdateTotalObjects()
    {
        totalObjects++;
        //Debug.Log("incereased Total Objects");
        //Debug.Log(totalObjects);
        if(totalObjects == potatoCount)
        {
            if(endScreenCo!=null)
                StopCoroutine(endScreenCo);
            
            endScreenCo = StartCoroutine(LoadEndScreen());
        }
    }

    
}
