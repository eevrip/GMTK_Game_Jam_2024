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
