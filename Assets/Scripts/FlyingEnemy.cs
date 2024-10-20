using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public AudioSource audioSrc;
    public AudioClip targetAquired;
 
    public AudioClip blowUp;
    public AudioSource flappingWings;
    public bool played;
    public LayerMask layerMask;
    private float timer = 0.2f;
    public float maxVolumeDistance = 24f;
    public float minVolumeDistance = 2f;
    public bool IsPlayerInFOV()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, 10f, layerMask);
        Debug.DrawRay(transform.position, (player.transform.position - transform.position).normalized * hit.distance, Color.yellow);
        if (hit.collider != null)
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                return true;
            }

        /*if(hit.collider !=null)
{
    Debug.Log("hitting things" + hit.collider.name + "player" + hit.distance);
}*/

        return false;
    }
    void Update()
    {
        pointA.transform.position = new Vector3(pointA.transform.position.x, transform.position.y, transform.position.z);
        pointB.transform.position = new Vector3(pointB.transform.position.x, transform.position.y, transform.position.z);
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (IsPlayerInFOV() && distanceToPlayer < 10f)
        {
            if (!attackingPlayer)
            {
                Debug.Log("Plau");
                audioSrc.clip = targetAquired;
                audioSrc.Play();
            }
            attackingPlayer = true; 
           
        }
        else
        {
            attackingPlayer = false;
        }

        float step = speed * Time.deltaTime;




        if (attackingPlayer)
        {
            // Aim at the player
            anim.SetBool("SeePlayer", true);
           
            //Vector3 direction = (player.transform.position - firepoint.position).normalized;
            // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // firepoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            Vector3 dir = (player.transform.position - transform.position).normalized;
            float ang = Mathf.Abs(Vector3.Dot(Vector2.right, dir));
            if(ang < 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step*3f);
            /* if (Vector3.Distance(transform.position, player.transform.position) < 2f)
             {
                 anim.SetTrigger("TouchPlayer");
             }*/
            // Shoot at the player every 2 seconds
            /*  shootTimer += Time.deltaTime;
               if (shootTimer >= shootInterval)
               {
                   Shoot(direction);
                   shootTimer = 0f;
               }*/
        }
        else
        {
            anim.SetBool("SeePlayer", false);
            played = false;
            if (movingToB)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointB.position, step);

                if (Vector3.Distance(transform.position, pointB.position) < 0.001f)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    movingToB = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, pointA.position, step);

                if (Vector3.Distance(transform.position, pointA.position) < 0.001f)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    movingToB = true;
                }
            }

        }

        AdjustFootstepVolume(distanceToPlayer);
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0.5f;
           
            flappingWings.Play();
        }
    }
    private void AdjustFootstepVolume(float distanceToPlayer)
    {
        float volume = Mathf.InverseLerp(maxVolumeDistance, minVolumeDistance, distanceToPlayer);
        flappingWings.volume = Mathf.Clamp(volume, 0f, 0.25f);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (attackingPlayer)  
           StartCoroutine(BlowUp());
    
    }
    public IEnumerator BlowUp()
    { anim.SetTrigger("TouchPlayer");
            audioSrc.clip = blowUp;
            audioSrc.Play();
       
        yield return new WaitForSeconds(0.65f);
        Destroy(gameObject);
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
           // targetAquired.Play();
        }
    }

}
