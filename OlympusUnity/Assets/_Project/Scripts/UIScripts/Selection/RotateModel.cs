using UnityEngine;

public class RotateModel : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 15f;

    public bool shouldRotate;
    
    void Update()
    {
        if (shouldRotate)
        {
            transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
        }
    }
}
