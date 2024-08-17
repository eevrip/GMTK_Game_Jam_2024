using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform firepoint;
    public float speed = 1.0f;
    public GameObject player;
    public GameObject bullet;
    public float bulletSpeed = 10.0f;

    private bool movingToB = false;
    private bool attackingPlayer;
    private float shootInterval = 2.0f;
    private float shootTimer;

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
            // Aim at the player
            Vector3 direction = (player.transform.position - firepoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            firepoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Shoot at the player every 2 seconds
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                Shoot(direction);
                shootTimer = 0f;
            }
        }
    }

    void Shoot(Vector3 direction)
    {
        GameObject newBullet = Instantiate(bullet, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
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
