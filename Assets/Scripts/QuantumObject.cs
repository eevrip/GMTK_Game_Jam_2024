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
    [Min(0.001f)]
    private float scalingFactor;
    [SerializeField]
    [Min(0.00001f)]
    private float massFactor;
    [SerializeField]
    private QuantumObjectsManager.Level minScaleLvl;

    [SerializeField]
    private QuantumObjectsManager.Level maxScaleLvl = QuantumObjectsManager.Level.Level5;


    [SerializeField]
    private QuantumObjectsManager.Level startingScaleLvl;

    private QuantumObjectsManager.Level currScaleLvl;
    [SerializeField]
    private GameObject EntangledVisuals;
    [SerializeField]
    private GameObject entanglementParticleSystem;
    [SerializeField]
   public bool isNaturallyEntangled = false;
    [SerializeField]
    public Vector2 startpos;

    private Rigidbody2D rb;

    [SerializeField]
    private bool canBeMoved;
    [SerializeField]
    private bool canKill;
    [SerializeField]
    private bool canDisappear;
    public bool CanDisappear => canDisappear;

    [SerializeField]
    private AudioSource audioSrc;
    private void Awake()
    {
        startpos = transform.position;
    }
    private void Start()
    {
        if (startingScaleLvl > maxScaleLvl)
        {
            startingScaleLvl = maxScaleLvl;
        }
        else if (startingScaleLvl < minScaleLvl)
        {
            startingScaleLvl = minScaleLvl;
        }

        currScaleLvl = startingScaleLvl;
        manager = QuantumObjectsManager.instance;
        rb = GetComponent<Rigidbody2D>();

        isNaturallyEntangled = (entangledObj != null);

        //Set the correct scale of the object
        float scaleXY = manager.LvlScale(currScaleLvl);
        transform.localScale = new Vector3(scaleXY * scalingFactor, scaleXY * scalingFactor, 1f);
        if (canBeMoved)
        {
            float currMass = manager.MassScale(currScaleLvl);
            rb.mass = currMass * massFactor;
        }
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
        if (QuantumObjectsManager.instance.entangledObjects.Contains(this) || isNaturallyEntangled)
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
        transform.localScale = new Vector3(scaleXY * scalingFactor, scaleXY * scalingFactor, 1f);
        if (canBeMoved)
        {
            float currMass = manager.MassScale(currScaleLvl);
            rb.mass = currMass * massFactor;
        }
    }

    //If reach min scale level return -1, if reach max scale level return 1
    public int ReachBoundary()
    {

        if (currScaleLvl == minScaleLvl)
            return -1;
        else if (currScaleLvl == maxScaleLvl)
            return 1;
        else
            return 0;
    }

    public void OnMouseDown()
    {
        if (isNaturallyEntangled)
            return;

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (canKill)
        {

            if (other.gameObject.CompareTag("Enemy"))
            {
                audioSrc.Play();
                
                Destroy(other.gameObject);
            }
        }
        else
            return;
    }
}
