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
    private List<GameObject> items;
    public enum ActivationItem { Bridge, MovingPlatform, Door};

    [SerializeField]
    private ActivationItem activationItem;
    [SerializeField]
    private bool hasUnpressedEffect;

    public AudioSource buttonPress;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        buttonPress.Play();
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
            foreach (GameObject item in items)
            {
                if (item)
                {
                    switch (activationItem)
                    {
                        case ActivationItem.Bridge:
                            item.GetComponent<Bridge>().ExtendBridge();
                            break;
                        case ActivationItem.MovingPlatform:
                            movingPlatform temp = item.GetComponent<movingPlatform>();
                            temp.IsMoving = !temp.IsMoving;
                            break;
                        case ActivationItem.Door:
                            item.GetComponent<Door>().OpenDoor();
                            break;
                        default:
                            break;
                    }

                }
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (hasUnpressedEffect)
        {
            foreach (GameObject item in items)
            {
                if (item)
                {
                    switch (activationItem)
                    {
                        case ActivationItem.Bridge:
                            item.GetComponent<Bridge>().ExtendBridge();
                            break;
                        case ActivationItem.MovingPlatform:
                            movingPlatform temp = item.GetComponent<movingPlatform>();
                            temp.IsMoving = !temp.IsMoving;
                            break;
                        case ActivationItem.Door:
                            item.GetComponent<Door>().OpenDoor();
                            break;
                        default:
                            break;
                    }
                }

            }
        }
        

    }




}
