using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject potato;
    [SerializeField] GameObject rock;
    [SerializeField] float timeBetweenSpawn;
    float timeToSpawn;
    MassRandomizer massRandomizer;
    Vector3 originalScaleRock, originalScalePotato;
    float originalMassRock,originalMassPotato;


    void Awake()
    {
        massRandomizer = FindAnyObjectByType<MassRandomizer>();
        originalScalePotato = new Vector3(0.5f,0.5f,0f);   
        originalScaleRock   = new Vector3(0.5f,0.5f,0f);  
        originalMassPotato = potato.GetComponent<Rigidbody2D>().mass;
        originalMassRock = rock.GetComponent<Rigidbody2D>().mass;

    }
    void Start()
    {
        timeToSpawn = 0;
    }

    void Update()
    {
        if(Time.time > timeToSpawn)
        {
            float rd = Random.value;

            if(rd > 0.65)
            {
                GameObject clone = Instantiate(potato, transform.position, quaternion.identity);
                massRandomizer.ChangeMass(clone, originalScalePotato, originalMassPotato);
            }
            else
            {
                GameObject clone = Instantiate(rock, transform.position, quaternion.identity);
                massRandomizer.ChangeMass(clone, originalScaleRock, originalMassRock);
            }

            timeToSpawn += timeBetweenSpawn;
        }
    }
}
