using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    [SerializeField] int potatoCount = 16;
    [SerializeField] TextMeshProUGUI lastChance;
    [SerializeField] TextMeshProUGUI timer;
    float timeTracker;
    bool endTriggered = false;

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
        lastChance.enabled = false;
        timer.enabled = false;
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
        if(totalObjects == potatoCount-1)
        {
            showEndUI();
        }

        if(totalObjects == potatoCount)
        {
            if(endScreenCo!=null)
                StopCoroutine(endScreenCo);
            
            endScreenCo = StartCoroutine(LoadEndScreen());
        }
    }

    private void showEndUI()
    {
        lastChance.enabled = true;
        timer.enabled = true;

        timeTracker = Time.time;
    }

    private void UpdateUI()
    {
        if (timer.enabled == true)
        {   
            float timeInFloat = 10 - (Time.time - timeTracker);
            int timeLeft = Mathf.CeilToInt(timeInFloat);
            timer.text = timeLeft.ToString();
            if(!endTriggered && timeInFloat <= 1.5f)
            {
                endTriggered = true;
                if(endScreenCo!=null)
                    StopCoroutine(endScreenCo);
            
                endScreenCo = StartCoroutine(LoadEndScreen());
            }
        }
    }

    void Update()
    {
        UpdateUI();
    }
}
