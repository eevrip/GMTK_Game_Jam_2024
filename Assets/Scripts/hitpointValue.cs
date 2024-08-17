using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitpointValue : MonoBehaviour
{
    public int hitpoints;
    public bool bullet;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (bullet)
        {
            Destroy(this.gameObject);
        }
    }
}
