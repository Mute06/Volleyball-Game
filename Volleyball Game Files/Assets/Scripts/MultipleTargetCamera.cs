using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = 0.5f;
    [Header("Zoom Values")]
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 10f;

    private Vector3 velocity;
    private Bounds currentBound;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }


    private void LateUpdate()
    {
        if (targets.Count == 0)
            return;

        GetBounds();
        Move();
        Zoom();

    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    private void Zoom()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance() / zoomLimiter);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    private void GetBounds()
    {

        var bounds = new Bounds(targets[0].position, Vector3.zero);

        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        currentBound = bounds;
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        return currentBound.center;
    }

    private float GetGreatestDistance()
    {
        return currentBound.size.x;
    }
}
