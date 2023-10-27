using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2.0f;

    private Transform currentTarget;

    void Start()
    {
        currentTarget = pointA;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            if (currentTarget == pointA)
                currentTarget = pointB;
            else
                currentTarget = pointA;
        }
    }
}
