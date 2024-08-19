using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playAudioPoints : MonoBehaviour
{

    public bool played;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !played)
        {
            this.GetComponent<AudioSource>().Play();
            played = true;
        }
    }
}
