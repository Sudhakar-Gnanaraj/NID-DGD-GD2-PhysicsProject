using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    [SerializeField] int potatoCount = 20;

    int potattoesSpawned = 0;
    int totalObjects = 0;
    Coroutine endScreenCo;

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
        if(totalObjects == potatoCount)
        {
            if(endScreenCo!=null)
                StopCoroutine(endScreenCo);
            
            endScreenCo = StartCoroutine(LoadEndScreen());
        }
    }
}
