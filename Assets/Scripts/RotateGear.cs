using UnityEngine;

public class RotateGear : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0f, 0f, 200f);

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
