using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuantumObject : MonoBehaviour
{
    [SerializeField]
    private int quantumObjId;
    [SerializeField]
    private List<QuantumObject> entangledObjs;
    [SerializeField]
    [Range(2, 5)]
    private int numScaleLvls;
    [SerializeField]
    private ScaleLevels.Level startingScaleLvl;

    private ScaleLevels.Level currScaleLvl;
    [SerializeField]
    private ScaleLevels lvls;



    void Awake()
    {

        currScaleLvl = startingScaleLvl;
        //Set the correct scale of the object
        float scaleXY = lvls.LvlScale(currScaleLvl);
        transform.localScale = new Vector3(scaleXY, scaleXY, 1f);

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*  public void AddEntangledObj(QuantumObject obj)
      {
          entangledObjs.Add(obj);
          entangledObjs[entangledObjs.Count].AddEntangledObj(this);
      }*/

    public void ChangeLevel(int i)
    {
        int[] alreadyChanged = new int[lvls.TotNumEntangledQuantumObjects]; //The total number of objects that are entangled together

        if (i < 0 && ReachBoundary() == -1)
            return;
        else if (i > 0 && ReachBoundary() == 1)
            return;

        /*  foreach (var obj in entangledObjs)
          {
              if (alreadyChanged[obj.quantumObjId] == 0)//if not checked
              {  

                  if (-i < 0 && obj.ReachBoundary() == -1)
                      return;
                  else if (-i > 0 && obj.ReachBoundary() == 1)
                      return;


                  obj.ToChangeLevel(-i, alreadyChanged);
              }
          } */
        ToChangeLevel(i, alreadyChanged);


    }
    public bool ToChangeLevel(int i, int[] alreadyChanged)
    {

        alreadyChanged[quantumObjId] = 1;
        foreach (var obj in entangledObjs)
        {
            if (alreadyChanged[obj.quantumObjId] == 0)//if not checked
            {

                if (-i < 0 && obj.ReachBoundary() == -1)
                    return false;
                else if (-i > 0 && obj.ReachBoundary() == 1)
                    return false;


                if (!obj.ToChangeLevel(-i, alreadyChanged))
                {
                    return false;

                }
            }
        }

        //if (alreadyChanged[quantumObjId] == 0)
        // {
        if (i < 0 && ReachBoundary() != -1)
            currScaleLvl--;
        else if (i > 0 && ReachBoundary() != 1)
            currScaleLvl++;

        float scaleXY = lvls.LvlScale(currScaleLvl);
        transform.localScale = new Vector3(scaleXY, scaleXY, 1f);
        return true;
        //  }



    }

    //If reach min scale level return -1, if reach max scale level return 1
    public int ReachBoundary()
    {

        if (currScaleLvl == ScaleLevels.Level.Level1)
            return -1;
        else if (currScaleLvl == (ScaleLevels.Level)(numScaleLvls - 1))
            return 1;
        else
            return 0;
    }
}
