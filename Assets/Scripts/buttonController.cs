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
    [SerializeField]
    [Tooltip("The item that the button is connected to")]
    private GameObject item;
    public enum ActivationItem { Bridge, MovingPlatform, Door};

    [SerializeField]
    private ActivationItem activationItem;

    

    private void OnTriggerEnter2D(Collider2D other)
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
            if (item)
            {
                switch(activationItem)
                {
                    case ActivationItem.Bridge:
                        this.item.GetComponent<Bridge>().ExtendBridge();
                        break;
                    case ActivationItem.MovingPlatform:
                        movingPlatform temp = this.item.GetComponent<movingPlatform>();
                        temp.IsMoving = !temp.IsMoving;
                        break;
                    case ActivationItem.Door:
                        this.item.GetComponent<Door>().OpenDoor();
                        break;
                    default:
                        break;
                }
               
            }
        }
        
    }



    

    
}
