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
            
        }

        if (currentLevel > 0)
        {
            continueButton.interactable = true;
        }

    }
    public void startNewGame()
    {
        // if theres a save, ask if want to wipe all existing data
        SceneManager.LoadScene(1);
    }
    public void Continue()
    {
        SceneManager.LoadScene(currentLevel+1);
    }

    public void loadLevel(int levelNum)
    {
        SceneManager.LoadScene(levelNum);
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
        // if level 1 is not complete, only level 1 can be intereactable
        // if level 1 is complete then level 2 can be interactable
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
}
