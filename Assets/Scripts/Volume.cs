using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Volume : MonoBehaviour
{
    private AudioSource audioSrc;
    private AudioSource audioSrc1;

    private static float sfxVolume = 1f;
    private static float musicVolume = 1f;

    public string type;


    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc1 = GetComponent<AudioSource>();

        if (PlayerPrefs.GetFloat("firstvolumetime") == 0)
        {
            PlayerPrefs.SetFloat("sfxvolume", 0.5f);
            PlayerPrefs.SetFloat("musicvolume", 0.5f);
            PlayerPrefs.SetFloat("firstvolumetime", 31);
        }
    }

    void Update()
    {
        if (type == "sfx")
        {
            audioSrc.volume = PlayerPrefs.GetFloat("sfxvolume");
        }
        if (type == "music")
        {
            audioSrc1.volume = PlayerPrefs.GetFloat("musicvolume");
        }





    }
    public void SetVolume(float vol)
    {

        sfxVolume = vol;
        PlayerPrefs.SetFloat("sfxvolume", vol);

    }
    public void SetVolume1(float vol)
    {
        musicVolume = vol;
        PlayerPrefs.SetFloat("musicvolume", vol);
    }
}
