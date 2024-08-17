using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuantumObjectsManager : MonoBehaviour
{
    public static QuantumObjectsManager instance;
    [SerializeField]
    private float[] sclLvls = new float[5];

  
    public enum Level { Level1, Level2, Level3, Level4, Level5, Count };

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
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
        return sclLvls[(int)lvl];
    }
}
