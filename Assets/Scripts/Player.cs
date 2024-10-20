using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Projectile;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject growProjectilePrefab;
    [SerializeField] private GameObject shrikProjectilePrefab;
    [SerializeField] private float projectileLaunchVelocity = 500f;
    [SerializeField] private float projectileLaunchTorque = 50f;
    [SerializeField] private Vector3 launchOffset = Vector3.one;
    [SerializeField] private float fireCooldown = 1f;
    private float fireCooldownTimer = 0;
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject entanglementRangeVisualizer;
    
    private static Vector2 lastCheckpoint;
    public static Vector2 LastCheckpoint { get { return lastCheckpoint; } set {  lastCheckpoint = value; } }

    [SerializeField]
    private GameObject timTheTardigradePrefab;
    public GameObject tim;

    public Animator anim;

    private static bool isNewLevelLoaded;
    public static bool IsNewLevelLoaded { get { return isNewLevelLoaded; } set { isNewLevelLoaded = value; } }

    public bool canUseGrowProjectile = false;

    public AudioSource entanglementSound;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
       

        tim = Instantiate(timTheTardigradePrefab);
        tim.transform.position = transform.position + tim.GetComponent<TimTheTardigrade>().timsDesiredOffset;

        if (!isNewLevelLoaded)
        {
            ResetPosition(lastCheckpoint);
        }

        else
        {
            lastCheckpoint = transform.position;
        } Debug.Log("last check = " +lastCheckpoint);
    }

    void Update()
    {
        if (QuantumObjectsManager.instance.isInEntanglementMode)
        {   fireCooldownTimer = 0f;
            entanglementRangeVisualizer.SetActive(true);
            
            if (Input.GetButtonDown("Fire1"))
            {
                entanglementSound.Play();
            }
            return;
            
        }
        else
        {   fireCooldownTimer += Time.deltaTime;
            entanglementRangeVisualizer.SetActive(false);
            
                if (fireCooldownTimer >= fireCooldown)
                {
                    if (Input.GetButtonDown("Fire2"))
                    {
                        if (growProjectilePrefab && canUseGrowProjectile)
                        {
                            FireProjectile(growProjectilePrefab);
                        }
                    }
                    else if (Input.GetButtonDown("Fire1") && !DialogueManager.Instance.sentenceAudioSource.isPlaying)
                    {
                        FireProjectile(shrikProjectilePrefab);
                    }
                
                }
        }
    }

    public void ResetPosition(Vector2 checkpoint)
    {
        transform.position = checkpoint;
    }

    void FireProjectile(GameObject projectilePrefab)
    {
       

        Debug.Log("Shoot");
      

        Vector3 mousePos = Input.mousePosition;
       

        
        Vector2 shootDirection = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        Vector2 righDir = Vector2.right;
        PlayerMovement plm = GetComponent<PlayerMovement>();
        if (plm != null)
        {
            if(!plm.IsFacingRight)
            {
                righDir = -Vector2.right ;
            }
        }
        Vector2 shootDirNor = shootDirection.normalized;
       
        float angle = Vector3.Dot(shootDirNor, righDir);

        if (angle > 0)
        {

            this.GetComponent<PlayerMovement>().playShoot();
            this.GetComponent<PlayerMovement>().shooting = true;
            Vector3 shootPos = transform.position + new Vector3(launchOffset.x * transform.localScale.x, launchOffset.y, launchOffset.z);
        GameObject proj = Instantiate(projectilePrefab, shootPos, transform.rotation);

            shootDirection.Normalize();
            Vector2 force = new Vector2(projectileLaunchVelocity * shootDirection.x, projectileLaunchVelocity * shootDirection.y);
            Vector2 ownVelocityForce = rb.velocity * rb.velocity * projectilePrefab.GetComponent<Rigidbody2D>().mass / 2;
            ownVelocityForce.x *= transform.localScale.x;
            Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
            projRb.AddRelativeForce(force + ownVelocityForce);
            projRb.AddTorque(projectileLaunchTorque);
        }

        fireCooldownTimer = 0;
    }

    public void PlayTalking()
    {
        tim.GetComponent<TimTheTardigrade>().PlayTalking();
    }

    public void EndTalking()
    {
        tim.GetComponent<TimTheTardigrade>().EndTalking();
    }
}
