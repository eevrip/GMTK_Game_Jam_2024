using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetLevel : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            //If the player has not pas a checkpoint yet reset the scene
            if (!player.ResetPosition())
            {

                Scene currentScene = SceneManager.GetActiveScene();

                SceneManager.LoadScene(currentScene.name);
            }
        }
    }
}
