using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introManager : MonoBehaviour
{
    public GameObject player;
    public GameObject Tim;
    public AudioSource timAudio1;
    public AudioSource timAudio2;
    public bool arrived;
    public bool timMove;

    private void Awake()
    {
        timAudio1.Play();
    }
    private void Start()
    {
        // cannot move
        player.GetComponent<PlayerMovement>().attacked = true;
        Tim = GameObject.FindGameObjectWithTag("Tim");
        Tim.GetComponent<TimTheTardigrade>().canMove = false;
    }

    private void Update()
    {
        if (!arrived)
        {
            player.GetComponent<PlayerMovement>().IsFacingRight = false;
            player.GetComponent<PlayerMovement>().anim.SetBool("runningL", true);
            player.transform.position = Vector2.MoveTowards(player.transform.position, new Vector2(-10, -2.25f), 5 * Time.deltaTime);
            Tim.transform.position = new Vector2(-12, -3.8f);
        }


        if (player.transform.position == new Vector3(-10, -2.25f, player.transform.position.z))
        {
            player.GetComponent<PlayerMovement>().anim.SetBool("runningL", false);
            player.GetComponent<PlayerMovement>().anim.SetBool("idleL", true);
            arrived = true;
            StartCoroutine(playTimIntro());
        }

        if (timMove)
        {           
            Tim.transform.position = Vector2.MoveTowards(Tim.transform.position, new Vector2(-12, 0f), 6 * Time.deltaTime);
        }

    }

    void playTimAudio()
    {
        timAudio2.Play();
    }

    IEnumerator playTimIntro()
    {
        yield return new WaitForSeconds(2.4f);
        timMove = true;
        playTimAudio();
        player.GetComponent<PlayerMovement>().attacked = false;
        StartCoroutine(stopIntro());
    }

    IEnumerator stopIntro()
    {
        yield return new WaitForSeconds(47f);
        Tim.GetComponent<TimTheTardigrade>().canMove = true;
        Destroy(this);
    }
}
