using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class QuantumObject : MonoBehaviour
{
    [SerializeField]
    public QuantumObject entangledObj;

    private QuantumObjectsManager manager;

    [SerializeField]
    private QuantumObjectsManager.Level startingScaleLvl;

    private QuantumObjectsManager.Level currScaleLvl;
    [SerializeField]
    private GameObject EntangledVisuals;
    [SerializeField]
    private GameObject entanglementParticleSystem;

    private void Start()
    {
        currScaleLvl = startingScaleLvl;
        manager = QuantumObjectsManager.instance;

        //Set the correct scale of the object
        float scaleXY = manager.LvlScale(currScaleLvl);
        transform.localScale = new Vector3(scaleXY, scaleXY, 1f);
    }

    private void Update()
    {
        if (entangledObj)
        {
            // Visuals
            Vector3 dir = entangledObj.transform.position - transform.position;
            entanglementParticleSystem.transform.position = new Vector3(transform.position.x, transform.position.y, -2);   // Push the particle system a bit infront of everything on z axis
            entanglementParticleSystem.transform.rotation = Quaternion.LookRotation(dir);
        }
        if (QuantumObjectsManager.instance.entangledObjects.Contains(this))
        {
            EntangledVisuals.SetActive(true);
        }
        else
        {
            EntangledVisuals.SetActive(false);
        }

    }

    public bool CanChangeScaleLevel(int i)
    {
        if (i < 0 && ReachBoundary() == -1)
            return false;
        else if (i > 0 && ReachBoundary() == 1)
            return false;
        else return true;
    }

    public void ChangeScaleLevel(int i)
    {
        
        if (i < 0 && ReachBoundary() != -1)
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
        else if (currScaleLvl == (QuantumObjectsManager.Level.Count - 1))
            return 1;
        else
            return 0;
    }

    public void OnMouseDown()
    {
        if (entangledObj)
        {
            QuantumObjectsManager.instance.Disentangle(this);
        }
        else
        {
            if (!QuantumObjectsManager.instance.TryToEntangle(this))
                QuantumObjectsManager.instance.Disentangle(this);
        }
    }
}
