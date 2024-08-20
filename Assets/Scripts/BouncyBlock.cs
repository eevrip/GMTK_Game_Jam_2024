using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBlock : MonoBehaviour
{
    public float bounciness = 10.0f;
    public AudioClip[] bouncyClips;
    public AudioSource bounceSound;

    private void OnCollisionEnter2D(Collision2D other)
    {
        int i = Random.Range(0, bouncyClips.Length);

        if (bounceSound)
        {
            bounceSound.clip = bouncyClips[i];
            bounceSound.Play();
        }
        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 bounceDirection = -other.relativeVelocity.normalized;

            rb.AddForce(bounceDirection * bounciness, ForceMode2D.Impulse);
        }
    }
    
}
