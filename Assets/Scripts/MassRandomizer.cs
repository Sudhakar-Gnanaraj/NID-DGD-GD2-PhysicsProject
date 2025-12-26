using UnityEngine;

public class MassRandomizer : MonoBehaviour
{
    [SerializeField] float upperMass = 1f;
    [SerializeField] float lowerMass = 5f;


    public void ChangeMass (GameObject obj, Vector3 scale, float mass)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        float rand = Random.Range(lowerMass, upperMass);

        obj.transform.localScale = scale * rand;
        rb.mass = mass * rand;
    }
}
