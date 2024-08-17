using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumObjectsManager : MonoBehaviour
{
    [SerializeField]
    private float[] sclLvls = new float[5];

  
    public enum Level { Level1, Level2, Level3, Level4, Level5 };

    void Awake()
    {

        



    }
    //Only two objects are entangled
    public void ChangeLevelOfEntangledObjs(QuantumObject target, QuantumObject entangled, int i)
    {
        if (target.CanChangeScaleLevel(i) && entangled.CanChangeScaleLevel(-i))
        {

            target.ChangeScaleLevel(i);
            entangled.ChangeScaleLevel(-i);
        }
    }

    public float LvlScale(Level lvl)
    {
        float scl = 0f;
        if (lvl == Level.Level1)
            scl = sclLvls[0];
        else if (lvl == Level.Level2)
            scl = sclLvls[1];
        else if (lvl == Level.Level3)
            scl = sclLvls[2];
        else if (lvl == Level.Level4)
            scl = sclLvls[3];
        else if (lvl == Level.Level5)
            scl = sclLvls[4];


        return scl;
    }
}
