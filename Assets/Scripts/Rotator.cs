using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateSpeed = 120f;

    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
}