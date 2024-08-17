using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBlock : MonoBehaviour
{
    public float bounciness = 10.0f;

    private void OnCollisionEnter2D(Collision2D other)
    {

        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 bounceDirection = -other.relativeVelocity.normalized;

            rb.AddForce(bounceDirection * bounciness, ForceMode2D.Impulse);
        }
    }
}
