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
            Vector2 relDir = other.gameObject.transform.position - this.transform.position;
            Vector2 relDirNor = relDir.normalized;
            
            float angleVer = Mathf.Abs(Vector3.Dot(Vector2.up, relDirNor));
            float angleHor = Mathf.Abs(Vector3.Dot(Vector2.right, relDirNor));
            float angleOffset = 0.1f;
           // Debug.Log(relVelY + " " + otherRb.mass+" " + breakingPoint);
            if (forceVal > breakingPoint
                || (otherRb.mass > breakingPoint && Mathf.Abs(1-angleVer)<angleOffset) 
                || (otherRb.mass > breakingPoint && Mathf.Abs(1 - angleHor) < angleOffset))
                Destroy(gameObject);
            }
        
    }
    private void OnCollisionStay2D(Collision2D other)
    {

        Rigidbody2D otherRb = other.gameObject.GetComponent<Rigidbody2D>();
        if (otherRb != null)
        {

           
            Vector2 relDir = other.gameObject.transform.position - this.transform.position;
            Vector2 relDirNor = relDir.normalized;

            float angle = Vector3.Dot(Vector2.up, relDirNor);
            float angleOffset = 0.1f;

            if (otherRb.mass > breakingPoint && Mathf.Abs(1 - angle) < angleOffset)
                Destroy(gameObject);
        }

    }
}
