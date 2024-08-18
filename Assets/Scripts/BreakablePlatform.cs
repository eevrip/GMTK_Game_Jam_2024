using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [Tooltip("set it above 50 so that the player won't be able to break it")]
    
    [SerializeField] [Min(1f)]private float breakingPoint;
   

    private void OnCollisionEnter2D(Collision2D other)
    {

        Rigidbody2D otherRb = other.gameObject.GetComponent<Rigidbody2D>();
        if (otherRb != null)
        {
           
            float relVelY = -other.relativeVelocity.y;
            float forceVal = otherRb.mass * relVelY;
            Debug.Log("vel= " + relVelY);
            if (forceVal > breakingPoint)
                Destroy(gameObject);
        }
    }
}
