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

    public GameObject playButton;
    public Sprite play1;
    public Sprite play2;
    public GameObject continueButtonObj;
    public Sprite continue1;
    public Sprite continue2;
    public GameObject levelsButton;
    public Sprite levels1;
    public Sprite levels2;
    public GameObject quitButton;
    public Sprite quit1;
    public Sprite quit2;
    public GameObject settingsButton;
    public Sprite settings1;
    public Sprite settings2;
    public GameObject backButtonObj;
    public GameObject backButtonObj1;
    public Sprite back1;
    public Sprite back2;
    public GameObject level1Button;
    public Sprite level1but;
    public Sprite level1but1;
    public GameObject level2Button;
    public Sprite level2but;
    public Sprite level2but1;

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
           // continueButton.interactable = true;
        }

        StartCoroutine(loadScreen());
    }
    public void startNewGame()
    {    
        StartCoroutine(loadnextLevel(1));       
    }
    public void Continue()
    {
        if(currentLevel+1 <= 2)
        {
            StartCoroutine(loadnextLevel(currentLevel + 1));
        }
        else
        {
            StartCoroutine(loadnextLevel(2));
        }
        
    }

    public void loadLevel(int levelNum)
    {
        SceneManager.LoadScene(levelNum);
        // StartCoroutine(loadnextLevel(levelNum));
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
    public void hoverPlayButton()
    {
        playButton.GetComponent<Image>().sprite = play2;
    }
    public void exitHoverPlayButton()
    {
        playButton.GetComponent<Image>().sprite = play1;
    }

    public void hoverContinueButton()
    {
        continueButtonObj.GetComponent<Image>().sprite = continue2;
    }
    public void exitHoverContinueButton()
    {
        continueButtonObj.GetComponent<Image>().sprite = continue1;
    }

    public void hoverLevelsButton()
    {
        levelsButton.GetComponent<Image>().sprite = levels2;
    }
    public void exitHoverLevelsButton()
    {
        levelsButton.GetComponent<Image>().sprite = levels1;
    }

    public void hoverquitButton()
    {
        quitButton.GetComponent<Image>().sprite = quit2;
    }
    public void exitHoverquitButton()
    {
        quitButton.GetComponent<Image>().sprite = quit1;
    }

    public void hoverSettingsButton()
    {
        settingsButton.GetComponent<Image>().sprite = settings2;
    }
    public void exitHoverSettingsButton()
    {
        settingsButton.GetComponent<Image>().sprite = settings1;
    }

    public void hoverBackButton()
    {
        backButtonObj.GetComponent<Image>().sprite = back2;
    }
    public void exitHoverBackButton()
    {
        backButtonObj.GetComponent<Image>().sprite = back1;
    }
    public void hoverBack1Button()
    {
        backButtonObj1.GetComponent<Image>().sprite = back2;
    }
    public void exitHoverBack1Button()
    {
        backButtonObj1.GetComponent<Image>().sprite = back1;
    }

    public void hoverlevel1Button()
    {
        level1Button.GetComponent<Image>().sprite = level1but1;
    }
    public void exitHoverlevel1Button()
    {
        level1Button.GetComponent<Image>().sprite = level1but;
    }

    public void hoverlevel2Button()
    {
        level2Button.GetComponent<Image>().sprite = level2but1;
    }
    public void exitHoverlevel2Button()
    {
        level2Button.GetComponent<Image>().sprite = level2but;
    }
}
