using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{


    [SerializeField] private int currentLevel;
    [SerializeField] private GameObject MainScreen;
    [SerializeField] private GameObject SettingsScreen;
    [SerializeField] private GameObject LevelsScreen;
    [SerializeField] private Button continueButton;
    private bool saveFound;

    public GameObject Manager;
    public bool[] levels;
    public Button[] levelButtons;

    public GameObject fade;

    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");
        levels = new bool[3];
        levels = Manager.GetComponent<manager>().levels;

        for (int i = 0; i < levels.Length; i++)
        {
            if (!levels[i])
            {
                currentLevel = i;
                break;
            }
            currentLevel = levels.Length - 1;
            
        }

        if (currentLevel > 0)
        {
            continueButton.interactable = true;
        }

        StartCoroutine(loadScreen());
    }
    public void startNewGame()
    {    
        StartCoroutine(loadnextLevel(1));       
    }
    public void Continue()
    {
        StartCoroutine(loadnextLevel(currentLevel+1));
    }

    public void loadLevel(int levelNum)
    {
        StartCoroutine(loadnextLevel(levelNum));
    }

    public void openSettings()
    {
        MainScreen.SetActive(false);
        SettingsScreen.SetActive(true);
        LevelsScreen.SetActive(false);
    }

    public void levelSelector()
    {
        MainScreen.SetActive(false);
        SettingsScreen.SetActive(false);
        LevelsScreen.SetActive(true);

        for (int i = 0; i < levelButtons.Length-1; i++)
        {
            if (!levels[0])
            {
                levelButtons[0].interactable = true;
                break;
            }
            else if (levels[i])
            {
                levelButtons[i].interactable = true;
                levelButtons[i+1].interactable = true;
            }
        }
    }

    public void backButton()
    {
        MainScreen.SetActive(true);
        SettingsScreen.SetActive(false);
        LevelsScreen.SetActive(false);
    }

    public void quit()
    {
        Application.Quit();
    }

    IEnumerator loadnextLevel(int levelnum)
    {
        fade.SetActive(true);
        fade.GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(1);
        fade.GetComponent<Animator>().SetTrigger("Fade");
        SceneManager.LoadScene(levelnum);
    }
    IEnumerator loadScreen()
    {
        yield return new WaitForSeconds(1);
        fade.SetActive(false);
    }
}
