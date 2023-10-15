using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private Vector3 offset = new Vector3(0, 0, -10);
    private float smoothTime = 0.125f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;

    [SerializeField] private float leftBoundary = -2.5f;
    [SerializeField] private float rightBoundary = 5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = player.position + offset;
        targetPosition.x = Mathf.Clamp(targetPosition.x, leftBoundary, rightBoundary);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}