using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject potato;
    [SerializeField] GameObject onion;
    [SerializeField] GameObject rock;

    [SerializeField] Sprite[] potatoes;
    [SerializeField] Sprite[] onions;
    [SerializeField] Sprite[] rocks;

    [SerializeField] float minTimeBetweenSpawn = 3f;
    [SerializeField] float maxTimeBetweenSpawn = 5f;
    [SerializeField] float spawnInterval = 2f;
    float nextTimeToSpawn;

    [SerializeField] string currentLevel;
    MassRandomizer massRandomizer;
    LevelManager levelManager;
    ObjectPool pool;

    Vector3 originalScale = new Vector3(0.5f, 0.5f, 0f);
    float originalMassPotato;
    float originalMassRock;

    Coroutine spawnCo;
    int totalPotato, totalOnion, potatoesSpawned = 0, onionsSpawned = 0;

    void Awake()
    {
        nextTimeToSpawn = Time.time + Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);

        massRandomizer = FindAnyObjectByType<MassRandomizer>();
        levelManager = FindAnyObjectByType<LevelManager>();
        pool = FindAnyObjectByType<ObjectPool>();

        originalMassPotato = potato.GetComponent<Rigidbody2D>().mass;
        originalMassRock = rock.GetComponent<Rigidbody2D>().mass;
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
            totalPotato = levelManager.getPotatoCount() / 2;
            totalOnion = levelManager.getPotatoCount() / 2;
        }

        potatoesSpawned = 0;
        onionsSpawned = 0;
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            Spawn();

            if (levelManager.hasFinishedSpawn())
            {
                Destroy(gameObject);
                yield break;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void Spawn()
    {
        if (nextTimeToSpawn <= Time.time)
        {
            if (currentLevel == "Level 1")
            {
                SpawnPotato();
            }
            else
            {
                float r = Random.Range(0, 100);

                if (r <= 50 && potatoesSpawned < totalPotato)
                    SpawnPotato();
                else if (r > 50 && onionsSpawned < totalOnion)
                    SpawnOnion();
                else if (potatoesSpawned < totalPotato)
                    SpawnPotato();
                else
                    SpawnOnion();
            }
            nextTimeToSpawn += Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
        }
        else
        {
            SpawnRock();
        }
    }

    void SpawnPotato()
    {
        GameObject clone = pool.SpawnFromPool("potato", transform.position, Quaternion.identity);

        clone.GetComponent<SpriteRenderer>().sprite = potatoes[randomizer()];
        massRandomizer.ChangeMass(clone, originalScale, originalMassPotato);

        levelManager.updatePotatoCount();
        potatoesSpawned++;
    }

    void SpawnOnion()
    {
        GameObject clone = pool.SpawnFromPool("onion", transform.position, Quaternion.identity);

        //clone.GetComponent<SpriteRenderer>().sprite = onions[randomizer()];
        massRandomizer.ChangeMass(clone, originalScale, originalMassPotato);

        levelManager.updatePotatoCount();
        onionsSpawned++;
    }

    void SpawnRock()
    {
        GameObject clone = pool.SpawnFromPool("rock", transform.position, Quaternion.identity);

        clone.GetComponent<SpriteRenderer>().sprite = rocks[randomizer()];
        massRandomizer.ChangeMass(clone, originalScale, originalMassRock);
    }

    int randomizer()
    {
        float r = Random.Range(0f, 100f);
        if (r <= 33) return 0;
        if (r <= 66) return 1;
        return 2;
    }
}
