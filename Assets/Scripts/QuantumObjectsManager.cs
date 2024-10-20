using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class QuantumObjectsManager : MonoBehaviour
{
    public static QuantumObjectsManager instance;
    [SerializeField]
    private float[] sclLvls = new float[(int)Level.Count];
    [SerializeField]
    private float[] massLvls = new float[(int)Level.Count];
    public bool isInEntanglementMode { get; private set; } = false;
    [SerializeField]
    private GameObject entanglementUI;
    public List<QuantumObject> entangledObjects { get; private set; } = new List<QuantumObject>();
    [SerializeField]
    private float entanglementRange = 20f;
    private Player player;
    [SerializeField]
    private GameObject entanglementParticleSystemPlayer;

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

        player = FindObjectOfType<Player>();

    }

    private void Update()
    {
        if(Input.GetButtonDown("EntanglementMode"))
        {
            entanglementUI.SetActive(!entanglementUI.activeSelf);
            isInEntanglementMode = entanglementUI.activeSelf;
        }
    }

    //Only two objects are entangled
    public void ChangeLevelOfEntangledObjs(QuantumObject target, int i)
    {
        QuantumObject entangled = target.entangledObj;
        if (entangled)
        {
            if (target.CanChangeScaleLevel(i) && entangled.CanChangeScaleLevel(-i))
            {
                target.ChangeScaleLevel(i);
                entangled.ChangeScaleLevel(-i);
            }
        }
    }

    public float LvlScale(Level lvl)
    {
        return sclLvls[(int)lvl];
    }
    public float MassScale(Level lvl)
    {
        return massLvls[(int)lvl];
    }

    public bool TryToEntangle(QuantumObject qo)
    {
        if (!isInEntanglementMode)
            return false;

        Collider2D[] castResult = Physics2D.OverlapCircleAll(player.transform.position, entanglementRange);
        bool found = false;
        foreach (Collider2D collider in castResult)
        {
            if(collider.GetComponent<QuantumObject>() == qo)
            {
                found = true;
                break;
            }
        }
        if (!found)
            return false;

        if(entangledObjects.Count < 2 && !entangledObjects.Contains(qo))
        {
            if (entangledObjects.Count == 1 )
            {
                qo.entangledObj = entangledObjects[0];
                entangledObjects[0].entangledObj = qo;
                Debug.Log("Entangled " + qo.gameObject + " and " + entangledObjects[0]);
            }

            Vector3 posPSPlayer = new Vector3(player.transform.position.x, player.transform.position.y, -2);   // Push the particle system a bit infront of everything on z axis
            Instantiate(entanglementParticleSystemPlayer, posPSPlayer, Quaternion.LookRotation(qo.transform.position - player.transform.position));

            entangledObjects.Add(qo);
            return true;
        }
        return false;
    }

    public bool Disentangle(QuantumObject qo)
    {
        if (!isInEntanglementMode)
            return false;

        //if (qo.entangledObj)
        //{
        //    Debug.Log("Disentangled " + qo.gameObject + " and " + qo.entangledObj.gameObject);
        //    qo.entangledObj.entangledObj = null;
        //}
        //else
        //{
        //    Debug.Log("Disentangled " + qo.gameObject);
        //}
        //qo.entangledObj = null;

        foreach (var obj in entangledObjects)
        {
            if (obj.entangledObj != null)
            {
                Debug.Log("Disentangled " + obj.gameObject + " and " + obj.entangledObj.gameObject);
                obj.entangledObj.entangledObj = null;
            }
            else Debug.Log("Disentangled " + obj.gameObject);
            obj.entangledObj = null;
        }
        entangledObjects.Clear();
        return true;
    }

    public void DisentangleNaturalObj(QuantumObject qo)
    {
        qo.entangledObj = null;
        qo.isNaturallyEntangled = false;
    }
}
