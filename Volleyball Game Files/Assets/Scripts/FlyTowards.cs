using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTowards : MonoBehaviour
{
    public bool destroyOnArrive;
    public bool killComponentOnArrive;
    public float speed = 10f;
    public Transform target { get; set; }

    private void Update()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, target.position, step);

        // When reached to the target
        if (Distance(transform.position,target.position) <= 0.1f)
        {
            if (destroyOnArrive) {
                Destroy(gameObject);
            }
            else if (killComponentOnArrive)
            {
                Destroy(this);
            }
        }
    }

    private float Distance(Vector3 position1, Vector3 position2)
    {
        Vector3 offset = position1 - position2;
        return offset.sqrMagnitude;
    }
}
