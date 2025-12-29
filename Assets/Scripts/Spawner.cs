using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject potato;
    [SerializeField] GameObject onion;
    [SerializeField] Sprite[] potatoes;
    [SerializeField] Sprite[] onions;

    [SerializeField] GameObject rock;
    [SerializeField] Sprite[] rocks;

    [SerializeField] float minTimeBetweenSpawn = 3f;
    [SerializeField] float maxTimeBetweenSpawn = 5f;
    [SerializeField] float spawnInterval = 2f;
    float nextTimeToSpawn;
    [SerializeField] string currentLevel;
    MassRandomizer massRandomizer;
    LevelManager levelManager;

    Vector3 originalScalePotato = new Vector3(0.5f, 0.5f, 0f);
    Vector3 originalScaleOnion = new Vector3(0.5f, 0.5f, 0f);
    Vector3 originalScaleRock   = new Vector3(0.5f, 0.5f, 0f);

    float originalMassPotato;
    float originalMassRock;

    Coroutine spawnCo;
    int totalPotato, totalOnion, potatoesSpawned = 0, onionsSpawned = 0;

    void Awake()
    {   nextTimeToSpawn = Time.time +Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
        massRandomizer = FindAnyObjectByType<MassRandomizer>();
        levelManager   = FindAnyObjectByType<LevelManager>();

        originalMassPotato = potato.GetComponent<Rigidbody2D>().mass;
        originalMassRock   = rock.GetComponent<Rigidbody2D>().mass;
    }

    void Start()
    {

        spawnCo = StartCoroutine(SpawnLoop());
        if (currentLevel == "Level 1")
        {
            totalPotato = levelManager.getPotatoCount();
            totalOnion = 0;
        }
        else
        {
            totalPotato = levelManager.getPotatoCount()/2;
            totalOnion = levelManager.getPotatoCount()/2;
        }
        potatoesSpawned = 0;
        onionsSpawned = 0;
    }

    IEnumerator SpawnLoop()
    {
        // Initial delay before first spawn
        //yield return new WaitForSeconds(1.5f);

        while (true)
        {
            Spawn();

            // Stop spawning if all potatoes are done
            if (levelManager.hasFinishedSpawn())
            {
                Destroy(gameObject);  // destroy spawner
                yield break;          // stop coroutine
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void Spawn()
    {
        /*float rd = Random.Range(0f, 100f);

        if (rd > 65)
        {
            // Spawn potato
            GameObject clone = Instantiate(potato, transform.position, quaternion.identity);
            massRandomizer.ChangeMass(clone, originalScalePotato, originalMassPotato);
            clone.GetComponent<SpriteRenderer>().sprite = potatoes[randomizer()];
            levelManager.updatePotatoCount();
        }
        else
        {
            // Spawn rock
            GameObject clone = Instantiate(rock, transform.position, quaternion.identity);
            massRandomizer.ChangeMass(clone, originalScaleRock, originalMassRock);
            clone.GetComponent<SpriteRenderer>().sprite = rocks[randomizer()];
        }*/
        if(nextTimeToSpawn <= Time.time)
        {   
            if(currentLevel == "Level 1")
            {
                // Spawn potato
                GameObject clone = Instantiate(potato, transform.position, quaternion.identity);
                massRandomizer.ChangeMass(clone, originalScalePotato, originalMassPotato);
                clone.GetComponent<SpriteRenderer>().sprite = potatoes[randomizer()];
                nextTimeToSpawn += Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
                levelManager.updatePotatoCount();
            }
            else
            {
                float r = Random.Range(0,100);

                if (r <= 50 && potatoesSpawned < totalPotato)
                {
                    // Spawn potato
                    GameObject clone = Instantiate(potato, transform.position, quaternion.identity);
                    massRandomizer.ChangeMass(clone, originalScalePotato, originalMassPotato);
                    clone.GetComponent<SpriteRenderer>().sprite = potatoes[randomizer()];
                    nextTimeToSpawn += Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
                    levelManager.updatePotatoCount();
                    potatoesSpawned++;
                }
                else if (r > 50 && onionsSpawned < totalOnion)
                {
                    // Spawn onion
                    GameObject clone = Instantiate(onion, transform.position, quaternion.identity);
                    massRandomizer.ChangeMass(clone, originalScalePotato, originalMassPotato);
                    //clone.GetComponent<SpriteRenderer>().sprite = onions[randomizer()];
                    nextTimeToSpawn += Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
                    levelManager.updatePotatoCount();
                    onionsSpawned++;
                }
                else if(potatoesSpawned < totalPotato)
                {
                    GameObject clone = Instantiate(potato, transform.position, quaternion.identity);
                    massRandomizer.ChangeMass(clone, originalScalePotato, originalMassPotato);
                    clone.GetComponent<SpriteRenderer>().sprite = potatoes[randomizer()];
                    nextTimeToSpawn += Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
                    levelManager.updatePotatoCount();
                }
                else
                {
                    GameObject clone = Instantiate(onion, transform.position, quaternion.identity);
                    massRandomizer.ChangeMass(clone, originalScalePotato, originalMassPotato);
                    //clone.GetComponent<SpriteRenderer>().sprite = potatoes[randomizer()];
                    nextTimeToSpawn += Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
                    levelManager.updatePotatoCount();
                }
            }
        }
        else
        {
            GameObject clone = Instantiate(rock, transform.position, quaternion.identity);
            massRandomizer.ChangeMass(clone, originalScaleRock, originalMassRock);
            clone.GetComponent<SpriteRenderer>().sprite = rocks[randomizer()];
        }

    }

    int randomizer()
    {
        float r = Random.Range(0f, 100f);

        if (r <= 33) return 0;
        else if (r <= 66) return 1;
        else return 2;
    }
}
