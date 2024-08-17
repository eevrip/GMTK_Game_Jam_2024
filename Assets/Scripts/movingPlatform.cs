using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB; 
    public float speed = 1.0f; 

    private bool movingToB = true;

    void Update()
    {
        float step = speed * Time.deltaTime;

        if (movingToB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, step);

            if (Vector3.Distance(transform.position, pointB.position) < 0.001f)
            {
                movingToB = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, step);

            if (Vector3.Distance(transform.position, pointA.position) < 0.001f)
            {
                movingToB = true;
            }
        }
    }
}
