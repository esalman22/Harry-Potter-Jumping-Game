using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class cameraController : MonoBehaviour
{
    public GameManager gameManager;
    //public TextMeshPro textSpeed;

    private float speed;
    public float speedMultiple = .05f;

    private Vector3 targetPosition;
    public Transform target;

    private float distance;

    public float startLimmit = 7f;
    public float maxDistanceBeforeLose = 7f;

    private bool stopCamera = false;

    private float timer;

    private Vector3 startPos;

    void Start()
    {
        timer = 0;
        startPos = transform.position;
    }

    void LateUpdate()
    {
        if (stopCamera) return;

        distance = target.position.y - transform.position.y;

        if (target.position.y < startLimmit)
            return;

        if (target.position.y + 13f < transform.position.y)
        {
            GameManager.instance.GameOver();
        }
        else if (distance > 1)
        {
            targetPosition = new Vector3(0, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, distance * Time.deltaTime);
        }
        else
        {
            targetPosition = new Vector3(0, transform.position.y + speed, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
        }

        timer += Time.deltaTime;
        //textSpeed.text = "x " + (int)(1 + (Time.time) / 60);
        speed = (1 + timer / 60) * speedMultiple;
    }

    public void StopCamera()
    {
        stopCamera = true;
    }

    public void ResetCamera()
    {
        // 👇 Reset position
        transform.position = startPos;

        // 👇 Reset flags & speed
        stopCamera = false;
        timer = 0;
        speed = 0f;
    }
}
