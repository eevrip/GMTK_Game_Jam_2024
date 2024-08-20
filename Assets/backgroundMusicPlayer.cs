using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backgroundMusicPlayer : MonoBehaviour
{
    public AudioClip[] backgroundMusic;
    public AudioSource backgroundMusicLines1;
    public AudioSource backgroundMusicLines2;
    public AudioClip previousClip;
    public int previousBuildIndex;

    private void Start()
    {
        previousClip = backgroundMusicLines1.clip;
        previousBuildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void changeMusic()
    {
        if(previousBuildIndex != SceneManager.GetActiveScene().buildIndex)
        {
            previousBuildIndex = SceneManager.GetActiveScene().buildIndex;
            if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1)
            {
                backgroundMusicLines1.Play();
                backgroundMusicLines2.Stop();
            }
            else
            {
                backgroundMusicLines2.Play();
                backgroundMusicLines1.Stop();
            }
        }
    }
}
