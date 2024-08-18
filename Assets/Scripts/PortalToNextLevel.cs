using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToNextLevel : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int nextIdx = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextIdx < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(nextIdx);
            else
                SceneManager.LoadScene(0);
        }
    }
}
