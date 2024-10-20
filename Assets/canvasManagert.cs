using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class canvasManagert : MonoBehaviour
{
    public GameObject Manager;
    public GameObject fade;
    public int levelNum;
   
    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");
        Manager.GetComponent<manager>().levels[levelNum] = true;
       

       
    }
    public void Home()

    {
        Time.timeScale = 1; 
        Manager.GetComponent<manager>().SaveManager();

        fade.GetComponent<Animator>().SetTrigger("Fade"); 
        StartCoroutine(loadnextLevel(0));
      //  SceneManager.LoadScene(0);
    }

    public void ResetLevel()
    {
        Time.timeScale = 1;
        Manager.GetComponent<manager>().SaveManager();

        fade.GetComponent<Animator>().SetTrigger("Fade");
          Player.IsNewLevelLoaded = true;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Scene currentScene = SceneManager.GetActiveScene();
        StartCoroutine(loadnextLevel(SceneManager.GetActiveScene().buildIndex));
    }
    
    IEnumerator loadnextLevel(int levelnum)
    {
        yield return new WaitForSeconds(1);
        Player.IsNewLevelLoaded = true;
        
        SceneManager.LoadScene(levelnum);
        
            
    }
}
