using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class FakeSpawn : MonoBehaviour
{   [SerializeField] int noOfVegetables = 1;
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
    Vector3 originalScaleRock   = new Vector3(0.2f, 0.2f, 0f);
    Coroutine spawnCo;
    float nextTimeToSpawn;
    MassRandomizer massRandomizer;
    float originalMassPotato;
    float originalMassRock;

    void Awake()
    {   nextTimeToSpawn = Time.time+ spawnInterval;
        massRandomizer = FindAnyObjectByType<MassRandomizer>();
        originalMassPotato = potato.GetComponent<Rigidbody2D>().mass;
        originalMassRock   = rock.GetComponent<Rigidbody2D>().mass;
    }

     void Start()
    {
        if(noOfVegetables ==3)
            spawnInterval = Random.Range(minTimeBetweenSpawn,maxTimeBetweenSpawn);
            
        spawnCo = StartCoroutine(SpawnLoop());
        
    }

    IEnumerator SpawnLoop()
    {
        // Initial delay before first spawn
        //yield return new WaitForSeconds(1.5f);

        while (true)
        {
            Spawn();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void Spawn()
    {
        float rd;
        if(noOfVegetables == 1)
        {
            rd = Random.Range(0f, 100f);
            if (rd < 50)
            {
                // Spawn potato
                GameObject clone = Instantiate(potato, transform.position, quaternion.identity);
                massRandomizer.ChangeMass(clone, originalScalePotato, originalMassPotato);
                clone.GetComponent<SpriteRenderer>().sprite = potatoes[randomizer()];
                //levelManager.updatePotatoCount();
            }
            else
            {
                // Spawn rock
                GameObject clone = Instantiate(rock, transform.position, quaternion.identity);
                massRandomizer.ChangeMass(clone, originalScaleRock, originalMassRock);
                clone.GetComponent<SpriteRenderer>().sprite = rocks[randomizer()];
            }
        }
        else if(noOfVegetables == 2)
        {
            rd = Random.Range(0f, 100f);
            if (rd < 33)
            {
                // Spawn potato
                GameObject clone = Instantiate(potato, transform.position, quaternion.identity);
                massRandomizer.ChangeMass(clone, originalScalePotato, originalMassPotato);
                clone.GetComponent<SpriteRenderer>().sprite = potatoes[randomizer()];
                //levelManager.updatePotatoCount();
            }
            else if (rd < 66)
            {
                GameObject clone = Instantiate(onion, transform.position, quaternion.identity);
                massRandomizer.ChangeMass(clone, originalScalePotato, originalMassPotato);
                //clone.GetComponent<SpriteRenderer>().sprite = onions[randomizer()];
                nextTimeToSpawn += Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
                //levelManager.updatePotatoCount();
                //onionsSpawned++;
            }
            else
            {
                // Spawn rock
                GameObject clone = Instantiate(rock, transform.position, quaternion.identity);
                massRandomizer.ChangeMass(clone, originalScaleRock, originalMassRock);
                clone.GetComponent<SpriteRenderer>().sprite = rocks[randomizer()];
            }
        }
        else
        {
            rd = Random.Range(0f, 100f);
            spawnInterval = Random.Range(minTimeBetweenSpawn,maxTimeBetweenSpawn);
            if (rd < 33)
            {
                // Spawn potato
                GameObject clone = Instantiate(potato, transform.position, quaternion.identity);
                massRandomizer.ChangeMass(clone, originalScalePotato, originalMassPotato);
                clone.GetComponent<SpriteRenderer>().sprite = potatoes[randomizer()];
                //levelManager.updatePotatoCount();
            }
            else if (rd < 66)
            {
                GameObject clone = Instantiate(onion, transform.position, quaternion.identity);
                massRandomizer.ChangeMass(clone, originalScalePotato, originalMassPotato);
                //clone.GetComponent<SpriteRenderer>().sprite = onions[randomizer()];
                nextTimeToSpawn += Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
                //levelManager.updatePotatoCount();
                //onionsSpawned++;
            }
            else
            {
                // Spawn rock
                GameObject clone = Instantiate(rock, transform.position, quaternion.identity);
                massRandomizer.ChangeMass(clone, originalScaleRock, originalMassRock);
                clone.GetComponent<SpriteRenderer>().sprite = rocks[randomizer()];
            }
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
