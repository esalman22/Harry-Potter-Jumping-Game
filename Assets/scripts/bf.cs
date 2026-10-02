using UnityEngine;

public class bf : MonoBehaviour
{
    public Transform cam;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - cam.position;
    }

    void LateUpdate()
    {
        Vector3 newPos = cam.position + offset;
        newPos.z = transform.position.z; // خلي Z ثابت
        transform.position = newPos;
    }
}
