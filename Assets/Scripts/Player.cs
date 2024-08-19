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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //lastCheckpoint = transform.position;

        GameObject tim = Instantiate(timTheTardigradePrefab);
        tim.transform.position = transform.position + tim.GetComponent<TimTheTardigrade>().timsDesiredOffset;
        if(lastCheckpoint != null)
        {
            ResetPosition(lastCheckpoint);
        }
    }

    void Update()
    {
        fireCooldownTimer += Time.deltaTime;

        if (QuantumObjectsManager.instance.isInEntanglementMode)
        {
            entanglementRangeVisualizer.SetActive(true);
            return; // No firing projectiles when in entanglement mode
        }
        else
        {
            entanglementRangeVisualizer.SetActive(false);
            if (fireCooldownTimer >= fireCooldown)
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    FireProjectile(growProjectilePrefab);
                }
                else if (Input.GetButtonDown("Fire1"))
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
        Vector3 shootPos = transform.position + new Vector3(launchOffset.x * transform.localScale.x, launchOffset.y, launchOffset.z);
        GameObject proj = Instantiate(projectilePrefab, shootPos, transform.rotation);

        Vector3 mousePos = Input.mousePosition;
        Vector2 shootDirection = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        shootDirection.Normalize();
        Vector2 force = new Vector2(projectileLaunchVelocity * shootDirection.x, projectileLaunchVelocity * shootDirection.y);
        Vector2 ownVelocityForce = rb.velocity * rb.velocity * projectilePrefab.GetComponent<Rigidbody2D>().mass / 2;
        ownVelocityForce.x *= transform.localScale.x;
        Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
        projRb.AddRelativeForce(force + ownVelocityForce);
        projRb.AddTorque(projectileLaunchTorque);

        fireCooldownTimer = 0;
    }
}
