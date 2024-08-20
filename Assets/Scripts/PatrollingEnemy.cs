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

    public AudioClip[] enemyFootsteps;
    public AudioSource footsteps;
    public float timer;
    public float maxVolumeDistance = 10f;
    public float minVolumeDistance = 2f;

    public Animator anim;

    private void Start()
    {
        timer = 0.2f;
    }
    void Update()
    {
        pointA.transform.position = new Vector3(pointA.transform.position.x, transform.position.y, transform.position.z);
        pointB.transform.position = new Vector3(pointB.transform.position.x, transform.position.y, transform.position.z);

        if (Vector3.Distance(transform.position, player.transform.position) < 10f)
        {
           // attackingPlayer = true;
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
                anim.SetBool("left", false);

                if (Vector3.Distance(transform.position, pointB.position) < 0.001f)
                {
                    movingToB = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, pointA.position, step);
                anim.SetBool("left", true);

                if (Vector3.Distance(transform.position, pointA.position) < 0.001f)
                {
                    movingToB = true;
                }
            }
        }
        else
        {
            //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        AdjustFootstepVolume(distanceToPlayer);
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            timer = 0.5f;
            int i = Random.Range(0, enemyFootsteps.Length);
            footsteps.clip = enemyFootsteps[i];
            footsteps.Play();
        }
    }
    private void AdjustFootstepVolume(float distanceToPlayer)
    {
        float volume = Mathf.InverseLerp(maxVolumeDistance, minVolumeDistance, distanceToPlayer);
        footsteps.volume = Mathf.Clamp(volume, 0f, 1f);
    }
}
