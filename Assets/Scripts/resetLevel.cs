using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class resetLevel : MonoBehaviour
{
    public static Vector2 lastCheckpoint;


    public void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();

            Scene currentScene = SceneManager.GetActiveScene();

            SceneManager.LoadScene(currentScene.name); 
        }
        else if (other.gameObject.CompareTag("Box"))
        {
            other.transform.position = other.gameObject.GetComponent<QuantumObject>().startpos;
        }
    }
}
