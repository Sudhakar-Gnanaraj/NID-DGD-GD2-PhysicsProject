using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class FakeSpawn : MonoBehaviour
{
    [SerializeField] int noOfVegetables = 1;

    [SerializeField] GameObject potato;
    [SerializeField] GameObject onion;
    [SerializeField] Sprite[] potatoes;
    [SerializeField] Sprite[] onions;

    [SerializeField] GameObject rock;
    [SerializeField] Sprite[] rocks;

    [SerializeField] float minTimeBetweenSpawn = 3f;
    [SerializeField] float maxTimeBetweenSpawn = 5f;
    [SerializeField] float spawnInterval = 2f;

    Vector3 originalScalePotato = new Vector3(0.2f, 0.2f, 0f);
    Vector3 originalScaleOnion = new Vector3(0.2f, 0.2f, 0f);
    Vector3 originalScaleRock = new Vector3(0.2f, 0.2f, 0f);

    float nextTimeToSpawn;

    MassRandomizer massRandomizer;
    ObjectPool pool;

    float originalMassPotato;
    float originalMassRock;

    Coroutine spawnCo;

    void Awake()
    {
        nextTimeToSpawn = Time.time + spawnInterval;

        massRandomizer = FindAnyObjectByType<MassRandomizer>();
        pool = FindAnyObjectByType<ObjectPool>();

        originalMassPotato = potato.GetComponent<Rigidbody2D>().mass;
        originalMassRock = rock.GetComponent<Rigidbody2D>().mass;
    }

    void Start()
    {
        if (noOfVegetables == 3)
            spawnInterval = Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);

        spawnCo = StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void Spawn()
    {
        float rd;

        if (noOfVegetables == 1)
        {
            rd = Random.Range(0f, 100f);

            if (rd < 50)
                SpawnPotato();
            else
                SpawnRock();
        }
        else if (noOfVegetables == 2)
        {
            rd = Random.Range(0f, 100f);

            if (rd < 33)
                SpawnPotato();
            else if (rd < 66)
                SpawnOnion();
            else
                SpawnRock();
        }
        else   // noOfVegetables == 3
        {
            rd = Random.Range(0f, 100f);
            spawnInterval = Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);

            if (rd < 33)
                SpawnPotato();
            else if (rd < 66)
                SpawnOnion();
            else
                SpawnRock();
        }
    }

    // -----------------------------
    //         SPAWN FUNCTIONS
    // -----------------------------

    void SpawnPotato()
    {
        GameObject clone = pool.SpawnFromPool("potato", transform.position, quaternion.identity);

        clone.GetComponent<SpriteRenderer>().sprite = potatoes[randomizer()];
        massRandomizer.ChangeMass(clone, originalScalePotato, originalMassPotato);
    }

    void SpawnOnion()
    {
        GameObject clone = pool.SpawnFromPool("onion", transform.position, quaternion.identity);

        //clone.GetComponent<SpriteRenderer>().sprite = onions[randomizer()];
        massRandomizer.ChangeMass(clone, originalScaleOnion, originalMassPotato);
    }

    void SpawnRock()
    {
        GameObject clone = pool.SpawnFromPool("rock", transform.position, quaternion.identity);

        clone.GetComponent<SpriteRenderer>().sprite = rocks[randomizer()];
        massRandomizer.ChangeMass(clone, originalScaleRock, originalMassRock);
    }

    // Random sprite picker
    int randomizer()
    {
        float r = Random.Range(0f, 100f);

        if (r <= 33) return 0;
        else if (r <= 66) return 1;
        else return 2;
    }
}
