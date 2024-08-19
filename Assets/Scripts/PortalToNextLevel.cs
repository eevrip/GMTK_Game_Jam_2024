using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToNextLevel : MonoBehaviour
{

    public GameObject Manager;
    public int levelNum;

    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Manager.GetComponent<manager>().levels[levelNum] = true;
            Manager.GetComponent<manager>().SaveManager();

           Player.IsNewLevelLoaded = true;
            int nextIdx = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextIdx < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(nextIdx);
            else
                SceneManager.LoadScene(0);
        }
    }
}
