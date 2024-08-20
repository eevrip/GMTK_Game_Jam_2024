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

    public Animator anim;
    public AudioSource targetAquired;
    public bool played;

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
        
        if(attackingPlayer)
        {
            // Aim at the player
            anim.SetBool("attacking", true);
            //targetAquired.Play();
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
        else
        {
            anim.SetBool("attacking", false);
            played = false;
        }
    }

    void Shoot(Vector3 direction)
    {
        GameObject newBullet = Instantiate(bullet, firepoint.position, Quaternion.identity);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }
    }

    void targetFound()
    {
        if (!played)
        {
            played = true;
            targetAquired.Play();
        }
    }
}
