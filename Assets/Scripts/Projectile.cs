using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public enum ProjectileType { Grow, Shrik}
	public ProjectileType projectileType = ProjectileType.Grow;
	private bool hasCollided = false;

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

		Debug.Log("Collision!");
		QuantumObject qo = collision.gameObject.GetComponent<QuantumObject>();
		if (qo)
		{
			hasCollided = true;
			switch (projectileType)
			{
				case ProjectileType.Grow:
					QuantumObjectsManager.instance.ChangeLevelOfEntangledObjs(qo, 1);
					break;
				case ProjectileType.Shrik:
					QuantumObjectsManager.instance.ChangeLevelOfEntangledObjs(qo, -1);
					break;
				default:
					break;
			}
		}

		Destroy(gameObject);
	}
}
