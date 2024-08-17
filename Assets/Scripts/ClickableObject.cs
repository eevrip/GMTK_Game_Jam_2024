using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ClickableObject : MonoBehaviour
{

    
    private QuantumObject obj;

    
    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            //obj.ChangeSize(2f);
            obj.ChangeLevel(1);
            
        }
        else if (Input.GetMouseButtonDown(1))
        {
            obj.ChangeLevel(-1);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<QuantumObject>();
        

    }


}
