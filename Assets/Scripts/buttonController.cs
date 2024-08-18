using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonController : MonoBehaviour
{
    public bool increaseSize;
    public int sizeValue;
    public int maxSize;
    public int minSize;
    public GameObject player;

    public bool affectsPlayer;
    public GameObject item;
    public enum ActivationItem { Bridge, MovingPlatform, Door};

    [SerializeField]
    private ActivationItem activationItem;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (affectsPlayer)
        {
            if (!increaseSize && sizeValue >= minSize)
            {
                player.transform.localScale = player.transform.localScale * 0.5f;
                sizeValue -= 1;
            }
            else if (sizeValue <= maxSize)
            {
                player.transform.localScale = player.transform.localScale * 2f;
                sizeValue += 1;
            }
        }
        else
        {
           if(item)
                item.SetActive(true);
        }
        
    }
}
