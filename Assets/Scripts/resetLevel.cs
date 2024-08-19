using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class resetLevel : MonoBehaviour
{
    public static Vector2 lastCheckpoint;


    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            if (!other.gameObject.GetComponent<QuantumObject>().CanDisappear)
            {
                other.transform.position = other.gameObject.GetComponent<QuantumObject>().startpos;
            }
            else
            {
                QuantumObjectsManager.instance.DisentangleNaturalObj(other.gameObject.GetComponent<QuantumObject>().entangledObj);
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
