using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 1.0f;
    public GameObject player;

    private bool movingToB = false;
    private bool attackingPlayer;

    void Update()
    {
        pointA.transform.position = new Vector3(pointA.transform.position.x, transform.position.y, transform.position.z);
        pointB.transform.position = new Vector3(pointB.transform.position.x, transform.position.y, transform.position.z);

        if (Vector3.Distance(transform.position, player.transform.position) < 10f)
        {
            attackingPlayer = true;
        }
        else
        {
            attackingPlayer = false;
        }

        float step = speed * Time.deltaTime;

        if (!attackingPlayer)
        {
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
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
