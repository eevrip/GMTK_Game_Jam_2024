using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public static buttonController.ActivationItem typeOfActivation { get; private set; } = buttonController.ActivationItem.Bridge;
    [SerializeField]private bool isExtended;
    public bool IsExtended { get { return isExtended; } set { isExtended = value; } }
    
   
    public void ExtendBridge()
    {
        if (isExtended)
        {
           
           gameObject.SetActive(false);
            isExtended = false;
        }
        else
        {
           
            gameObject.SetActive(true);
            isExtended = true;
        }
    }
}
