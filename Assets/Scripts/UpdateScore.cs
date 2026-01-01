using System.Collections;
using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    ScoreManager scoreManager;
    AudioPlayer audioPlayer;
    LevelManager levelManager;
    //SmoothShake smoothShake;
    [SerializeField] GameObject ScorePopUpObject;
    //[SerializeField] Transform popUpLocation;
    [SerializeField] string compareTag;
    TextMeshProUGUI scorePopUpText;

    void Awake()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
        audioPlayer = FindAnyObjectByType<AudioPlayer>();
        levelManager = FindAnyObjectByType<LevelManager>();
        //smoothShake = FindAnyObjectByType<SmoothShake>();
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(compareTag))
        {
            scoreManager.ChangeScore(true,10);
            audioPlayer.PlayCorrectClip();
            ScorePopUp(10, Color.green, collision.transform);
            levelManager.UpdateTotalObjects();
        }
        else if (collision.CompareTag("Rock"))
        {
            scoreManager.ChangeScore(false,10);
            audioPlayer.PlayWrongClip();
            ScorePopUp(10, Color.red, collision.transform);
            if(collision.CompareTag("Potato") || collision.CompareTag("Onion"))
                levelManager.UpdateTotalObjects();
        }
        else
        {
            scoreManager.ChangeScore(true,2);
            audioPlayer.PlayCorrectClip();
            ScorePopUp(2, Color.green, collision.transform);
            if(collision.CompareTag("Potato") || collision.CompareTag("Onion"))
                levelManager.UpdateTotalObjects();
        }
    
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        CombinedShaker combinedShaker = GetComponentInParent<CombinedShaker>();

        if(combinedShaker != null)
        {
            if(collision.gameObject.GetComponent<ShakeChecker>().HasShaken())
            return;
        
            smoothShake.Shake();
            collision.gameObject.GetComponent<ShakeChecker>().SetShaken();
        }
        else
        {
            combinedShaker.OnChildCollision(collision);
        }
    }*/

    void ScorePopUp(int points, Color color, Transform popUpLocation)
    {
        Vector3 spawnPos = popUpLocation.position + new Vector3(0,1,0);
        GameObject scorePop = Instantiate(ScorePopUpObject, spawnPos, Quaternion.identity);
        scorePopUpText = scorePop.GetComponentInChildren<TextMeshProUGUI>();
        Rigidbody2D rb = scorePop.GetComponent<Rigidbody2D>();

        rb.linearVelocityY = Random.Range(2, 4);
        rb.linearVelocityX = Random.Range(-3, 3);
        float angle = Random.Range(-15f, 15f);
        rb.transform.rotation = Quaternion.Euler(0, 0, angle);

        if(color == Color.green)
            scorePopUpText.text = "+"+points.ToString();
        else
            scorePopUpText.text ="-"+ points.ToString();
        
        //scorePopUpText.text = points.ToString();
        scorePopUpText.color = color;
        //StartCoroutine(deleter(scorePop));
    }

    IEnumerator deleter(GameObject obj)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(obj);
    }
}
