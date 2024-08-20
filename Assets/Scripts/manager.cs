using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class manager : MonoBehaviour
{
    [Header("Audio")]
    public float mainVolume;
    public float sfxVolume;
    public float musicVolume;

    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider masterSlider;

    public GameObject settings;

    private static manager instance;

    public GameObject audioManager;

    public bool loadAudioLevels;

    [Header("Save System")]
    public bool[] levels;

    public bool deathVoiceLineActive;
    public AudioSource deathVoice;
    public AudioClip[] deathVoiceLines;

    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
        LoadManager();
        
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        // Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settings.activeSelf)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
            settings.SetActive(!settings.activeSelf);            
        }

        // On Load, Load audio
        if (loadAudioLevels)
        {
            mainVolume = audioManager.GetComponent<audioManager>().mainVolume;
            sfxVolume = audioManager.GetComponent<audioManager>().sfxVolume;
            musicVolume = audioManager.GetComponent<audioManager>().musicVolume;
        }

        if (deathVoiceLineActive)
        {
            StartCoroutine(playDeathVoice());
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        loadAudioLevels = false;
        Debug.Log("audio paused");
        this.GetComponent<backgroundMusicPlayer>().changeMusic();
        loadScene();
    }

    // Sets the slider values to whatever was saved/set
    void loadScene()
    {
        
        GameObject musicSliderObject = GameObject.Find("musicVolume");
        GameObject sfxSliderObject = GameObject.Find("sfxVolume");
        GameObject masterSliderObject = GameObject.Find("masterVolume");
        settings = GameObject.Find("Settings");
        audioManager = GameObject.Find("audioManager");

        if (musicSliderObject != null)
        {
            musicSlider = musicSliderObject.GetComponent<Slider>();
        }

        if (sfxSliderObject != null)
        {
            sfxSlider = sfxSliderObject.GetComponent<Slider>();
        }

        if (masterSliderObject != null)
        {
            masterSlider = masterSliderObject.GetComponent<Slider>();
        }

        if (musicSlider == null || sfxSlider == null || masterSlider == null)
        {
            Debug.LogWarning("One or more sliders could not be found in the current scene.");
        }
        else
        {
            Debug.Log("Sliders successfully assigned.");
        }

        musicSlider.value = musicVolume;
        audioManager.GetComponent<audioManager>().musicVolume = musicVolume;
        sfxSlider.value = sfxVolume;
        audioManager.GetComponent<audioManager>().sfxVolume = sfxVolume;
        masterSlider.value = mainVolume;
        audioManager.GetComponent<audioManager>().mainVolume = mainVolume;

        if (settings.activeSelf)
        {
            settings.SetActive(false);
        }
        loadAudioLevels = true;
    }

    public IEnumerator playDeathVoice()
    {
        yield return new WaitForSeconds(1);
        int i = Random.Range(0, deathVoiceLines.Length);
        deathVoice.clip = deathVoiceLines[i];
        deathVoice.Play();
        deathVoiceLineActive = false;
    }

    public void SaveManager()
    {
        SaveSystem.SaveManager(this);
    }
    public void LoadManager()
    {
        LevelData data = SaveSystem.LoadManager();
        if (data!= null)
        {
            levels[0] = data.levels[0];
            levels[1] = data.levels[1];
            levels[2] = data.levels[2];
        }
    }
}
