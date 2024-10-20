using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class canvasManagert : MonoBehaviour
{
    public GameObject fade;

    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetLevel()
    {
      //  Player.IsNewLevelLoaded = true;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Scene currentScene = SceneManager.GetActiveScene();
        loadLevel(currentScene.buildIndex);
    }
    IEnumerator loadLevel(int levelnum)
    {
        fade.SetActive(false);
        fade.GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(1);
        fade.GetComponent<Animator>().SetTrigger("Fade");
        SceneManager.LoadScene(levelnum);
    }
}
