using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.transform.tag == "Player")
            {
                Player.LastCheckpoint = transform.position + new Vector3(0f, 1.57f, 0f);
            }
        }
    }
}
