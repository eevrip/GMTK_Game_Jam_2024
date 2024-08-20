using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class Projectile : MonoBehaviour
{
	public enum ProjectileType { Grow, Shrink}
	public ProjectileType projectileType = ProjectileType.Shrink;
	private bool hasCollided = false;
    
    [SerializeField]
    private GameObject explosionVfx;

    void Start()
	{
        
    }

	void Update()
	{
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (hasCollided)
			return;

		//Debug.Log("Collision!");
		QuantumObject qo = collision.gameObject.GetComponent<QuantumObject>();
		if (qo)
		{
			hasCollided = true;
			switch (projectileType)
			{
				case ProjectileType.Grow:
					QuantumObjectsManager.instance.ChangeLevelOfEntangledObjs(qo, 1);
					break;
				case ProjectileType.Shrink:
					QuantumObjectsManager.instance.ChangeLevelOfEntangledObjs(qo, -1);
					break;
				default:
					break;
			}
		}

        Instantiate(explosionVfx, transform.position, transform.rotation);

		//StartCoroutine(DelayedDestroy());
		Destroy(gameObject);
	}

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
