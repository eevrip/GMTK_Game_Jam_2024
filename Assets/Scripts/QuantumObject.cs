using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuantumObject : MonoBehaviour
{
    public int quantumObjId;
    [SerializeField]
    private List<QuantumObject> entangledObjs;
    [SerializeField]
    [Range (2,5)]
    private int numScaleLvls;
    [SerializeField]
    private ScaleLevels.Level startingScaleLvl;

    public ScaleLevels.Level currScaleLvl;
    [SerializeField]
    private ScaleLevels lvls;


    public int totNumQuantumObjects;
   /* private float maxScale = 8f;
    
    private float minScale = 0.5f;
    private bool setToMaxScale = false;
    private bool setToMinScale = false;*/
    // Start is called before the first frame update
    void Awake()
    {
        
        currScaleLvl = startingScaleLvl;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //The scalingFactor must be greater than 0. If is equal to 1 nothing will happen 
   /* public void ChangeSize(float scalingFactor)
    {
       

        if(scalingFactor <0f || scalingFactor == 1f)
        {
            
            return;
        }
        float scaleX = scalingFactor * transform.localScale.x;

        float scaleY = scalingFactor * transform.localScale.y;

        if(scaleY >= maxScale)
        {
            scaleY = maxScale;
            setToMaxScale = true;
        }
        else if(scaleY <= minScale)
        {
            scaleY = minScale;
            setToMinScale = true;
        }

        if(scaleX > maxScale)
        {
            scaleX = maxScale;
        }
        else if(scaleX < minScale)
        {
            scaleX = minScale;
        }
        transform.localScale = new Vector3(scaleX,scaleY,1f);
        
        //Affect the size of the entangledBoxes
        foreach (var obj in entangledObjs)
        {
            
           // obj.ChangeSize(1f/scalingFactor);
        }
    }*/

  /*  public void AddEntangledObj(QuantumObject obj)
    {
        entangledObjs.Add(obj);
        entangledObjs[entangledObjs.Count].AddEntangledObj(this);
    }*/

    public void ChangeLevel(int i)
    {
        int [] alreadyChanged = new int[totNumQuantumObjects];

        ToChangeLevel(i, alreadyChanged);

    }
    public void ToChangeLevel(int i, int [] alreadyChanged)
    {
        alreadyChanged[quantumObjId] = 1;
       foreach (var obj in entangledObjs)
        {
            
             
                if (-i < 0 && obj.ReachBoundary() == -1)
                    return;
                else if (-i > 0 && obj.ReachBoundary() == 1)
                    return;
                
              if (alreadyChanged[obj.quantumObjId] == 0)//if not checked
            {    
                obj.ToChangeLevel(-i, alreadyChanged);
            }
        } 
       
        if (i < 0 && ReachBoundary() != -1)
            currScaleLvl--;
        else if(i > 0 && ReachBoundary() !=1)
            currScaleLvl++;
        
            float scaleXY = lvls.LvlScale(currScaleLvl);
            transform.localScale = new Vector3(scaleXY, scaleXY, 1f);
        
    }

    //If reach min scale level return -1, if reach max scale level return 1
    public int ReachBoundary() {

        if (currScaleLvl == ScaleLevels.Level.Level1)
            return -1;
        else if(currScaleLvl == (ScaleLevels.Level)(numScaleLvls-1))
            return 1;
        else
            return 0;
    }
}
