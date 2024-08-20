using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class audioManager : MonoBehaviour
{

    public float mainVolume;
    public float sfxVolume;
    public float musicVolume;

    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider masterSlider;

    public AudioSource hoverSound;
    public AudioSource clickSound;
    public AudioSource playAudioSound;

    public Sprite high;
    public Sprite med;
    public Sprite low;

    public GameObject mastersoundsicon;
    public GameObject musicsoundsicon;
    public GameObject sfxsoundsicon;

    private void Update()
    {
        if (!mastersoundsicon)
            return;
        if (!musicsoundsicon)
            return;
        if (!sfxsoundsicon)
            return;

        if (masterSlider.value == 1)
        {
            mastersoundsicon.GetComponent<Image>().sprite = high;
        }
        else if(masterSlider.value < 1 && masterSlider.value > 0.3)
        {

            mastersoundsicon.GetComponent<Image>().sprite = med;
        }
        else
        {
            mastersoundsicon.GetComponent<Image>().sprite = low;
        }

        if (musicSlider.value == 1)
        {
            musicsoundsicon.GetComponent<Image>().sprite = high;
        }
        else if (musicSlider.value < 1 && musicSlider.value > 0.3)
        {
            musicsoundsicon.GetComponent<Image>().sprite = med;
        }
        else
        {
            musicsoundsicon.GetComponent<Image>().sprite = low;
        }

        if (sfxSlider.value == 1)
        {
            sfxsoundsicon.GetComponent<Image>().sprite = high;
        }
        else if (sfxSlider.value < 1 && sfxSlider.value > 0.3)
        {
            sfxsoundsicon.GetComponent<Image>().sprite = med;
        }
        else
        {
            sfxsoundsicon.GetComponent<Image>().sprite = low;
        }

    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        musicVolume = volume;
        mixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        sfxVolume = volume;
        mixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
    }
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        mainVolume = volume;
        mixer.SetFloat("master", Mathf.Log10(volume) * 20);
    }

    public void playHover()
    {
        hoverSound.Play();
    }
    public void click()
    {
        clickSound.Play();
    }
    public void playSound()
    {
        playAudioSound.Play();
    }
}
