using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuantumObject : MonoBehaviour
{
    [SerializeField]
    private int quantumObjId;
    [SerializeField]
    private QuantumObject entangledObj;
    [SerializeField]
    [Range(2, 5)]
    private int numScaleLvls;
    [SerializeField]
    private QuantumObjectsManager manager;
    public QuantumObjectsManager Manager => manager;
    [SerializeField]
    private QuantumObjectsManager.Level startingScaleLvl;

    private QuantumObjectsManager.Level currScaleLvl;
  
   

    void Awake()
    {
        quantumObjId = transform.GetSiblingIndex();
        currScaleLvl = startingScaleLvl;
        //Set the correct scale of the object
        float scaleXY = manager.LvlScale(currScaleLvl);
        transform.localScale = new Vector3(scaleXY, scaleXY, 1f);
         

    }

    //------------------
    //This is a temp function to see if it works. The manager.ChangeLevelOfEntangledObjs() will be called when the bullet collides with the collider of the respected quantumObj
    //----------------------------
    public void testTempScale(int i)
    {
        manager.ChangeLevelOfEntangledObjs(this, entangledObj, i);
    }
    //--------------------------------------
    //-----------------------------------------------
   
    
    
    public void ChangeScaleLevel(int i)
    {
        if (i < 0 && ReachBoundary() == -1)
            return;
        else if (i > 0 && ReachBoundary() == 1)
            return;
        else if (i < 0 && ReachBoundary() != -1)
            currScaleLvl--;
        else if (i > 0 && ReachBoundary() != 1)
            currScaleLvl++;

        float scaleXY = manager.LvlScale(currScaleLvl);
        transform.localScale = new Vector3(scaleXY, scaleXY, 1f);
        
    }


    

    //If reach min scale level return -1, if reach max scale level return 1
    public int ReachBoundary()
    {

        if (currScaleLvl == QuantumObjectsManager.Level.Level1)
            return -1;
        else if (currScaleLvl == (QuantumObjectsManager.Level)(numScaleLvls - 1))
            return 1;
        else
            return 0;
    }
}
