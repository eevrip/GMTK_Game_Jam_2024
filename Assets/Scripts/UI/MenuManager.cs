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


    private void Start()
    {
        if (!saveFound)
        {
            continueButton.interactable = false;
        }
    }
    public void startNewGame()
    {
        // if theres a save, ask if want to wipe all existing data
        SceneManager.LoadScene(0);
    }
    public void Continue()
    {
        SceneManager.LoadScene(currentLevel);
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
