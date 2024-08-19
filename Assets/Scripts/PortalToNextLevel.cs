using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToNextLevel : MonoBehaviour
{

    public GameObject Manager;
    public GameObject fade;
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

            fade.GetComponent<Animator>().SetTrigger("Fade");

            StartCoroutine(loadnextLevel());
        }
    }

    IEnumerator loadnextLevel()
    {
        yield return new WaitForSeconds(1);
        Player.IsNewLevelLoaded = true;
        int nextIdx = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIdx < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextIdx);
        else
            SceneManager.LoadScene(0);
    }
}
