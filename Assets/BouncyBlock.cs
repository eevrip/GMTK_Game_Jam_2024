using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBlock : MonoBehaviour
{
    public float bounciness = 10.0f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the colliding object has a Rigidbody2D component
        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Calculate the opposite direction of the current velocity
            Vector2 bounceDirection = -other.relativeVelocity.normalized;

            // Apply force in the opposite direction
            rb.AddForce(bounceDirection * bounciness, ForceMode2D.Impulse);
        }
    }
}
