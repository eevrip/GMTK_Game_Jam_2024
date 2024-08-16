using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileLaunchVelocity = 500f;
    [SerializeField] private Vector3 launchOffset = Vector3.one;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 shootPos = transform.position + new Vector3(launchOffset.x * transform.localScale.x, launchOffset.y, launchOffset.z);
            GameObject proj = Instantiate(projectilePrefab, shootPos, transform.rotation);
            proj.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(projectileLaunchVelocity * transform.localScale.x, 0, 0));
        }
    }
}
