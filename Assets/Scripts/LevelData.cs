using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public bool[] levels;

    public LevelData (manager Manager)
    {
        levels = new bool[3];
        levels[0] = Manager.levels[0];
        levels[1] = Manager.levels[1];
        levels[2] = Manager.levels[2];
    }
}
