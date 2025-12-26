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
                Instantiate(potato,transform.position,quaternion.identity);
            else
                Instantiate(rock,transform.position,quaternion.identity);

            timeToSpawn += timeBetweenSpawn;
        }
    }
}
