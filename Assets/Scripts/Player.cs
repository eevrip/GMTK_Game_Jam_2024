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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        fireCooldownTimer += Time.deltaTime;

        if (QuantumObjectsManager.instance.isInEntanglementMode)
            return;

        if (fireCooldownTimer >= fireCooldown)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                FireProjectile(growProjectilePrefab);
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                FireProjectile(shrikProjectilePrefab);
            }
        }
    }

    void FireProjectile(GameObject projectilePrefab)
    {
        Vector3 shootPos = transform.position + new Vector3(launchOffset.x * transform.localScale.x, launchOffset.y, launchOffset.z);
        GameObject proj = Instantiate(projectilePrefab, shootPos, transform.rotation);

        Vector3 mousePos = Input.mousePosition;
        Vector2 shootDirection = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        shootDirection.Normalize();
        Vector2 force = new Vector2(projectileLaunchVelocity * shootDirection.x, projectileLaunchVelocity * shootDirection.y);
        Vector2 ownVelocityForce = rb.velocity * rb.velocity * projectilePrefab.GetComponent<Rigidbody2D>().mass / 2;
        ownVelocityForce.x *= transform.localScale.x;
        Debug.Log(ownVelocityForce);
        Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
        projRb.AddRelativeForce(force + ownVelocityForce);
        projRb.AddTorque(projectileLaunchTorque);

        fireCooldownTimer = 0;
    }
}
