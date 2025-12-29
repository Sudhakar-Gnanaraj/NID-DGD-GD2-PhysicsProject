using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject potato;
    [SerializeField] Sprite[] potatoes;

    [SerializeField] GameObject rock;
    [SerializeField] Sprite[] rocks;

    [SerializeField] float timeBetweenSpawn = 1f;

    MassRandomizer massRandomizer;
    LevelManager levelManager;

    Vector3 originalScalePotato = new Vector3(0.5f, 0.5f, 0f);
    Vector3 originalScaleRock   = new Vector3(0.5f, 0.5f, 0f);

    float originalMassPotato;
    float originalMassRock;

    Coroutine spawnCo;

    void Awake()
    {
        massRandomizer = FindAnyObjectByType<MassRandomizer>();
        levelManager   = FindAnyObjectByType<LevelManager>();

        originalMassPotato = potato.GetComponent<Rigidbody2D>().mass;
        originalMassRock   = rock.GetComponent<Rigidbody2D>().mass;
    }

    void Start()
    {
        spawnCo = StartCoroutine(SpawnLoop());
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

            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

    void Spawn()
    {
        float rd = Random.Range(0f, 100f);

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
