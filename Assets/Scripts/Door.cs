using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static buttonController.ActivationItem typeOfActivation { get; private set; } = buttonController.ActivationItem.Door;
    [SerializeField] private bool isOpen;
  //  public bool IsOpen { get { return isOpen; } set { isOpen = value; } }


    public void OpenDoor()
    {
        if (isOpen)
        {
            gameObject.SetActive(false);
            isOpen = false;
        }
        else
        {
            gameObject.SetActive(true);
            isOpen = true;
        }
    }
}
