using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    public Slider sfxvolume;
    public Slider musicvolume;
    void Start()
    {

    }

    void Update()
    {
        sfxvolume.value = PlayerPrefs.GetFloat("sfxvolume");
        musicvolume.value = PlayerPrefs.GetFloat("musicvolume");
    }
}
