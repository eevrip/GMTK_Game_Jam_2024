using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePlayerGrowProjectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().canUseGrowProjectile = true;
            Destroy(this);
        }
    }
}
