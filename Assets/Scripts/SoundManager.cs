using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip PickUpSoundEffect, LoseMenuTrigger, SesEfekti1, OlmeSesiDaha, HasarAlma, SpawnSesi, YurumeSesi,
        SakizPatlama , BalonPatlat, BalonSisirme , SakizCigneme ,END1 , END2, END3; 
    static AudioSource audioSrc;
    void Start()
    {
        PickUpSoundEffect = Resources.Load<AudioClip>("PickUpSoundEffect");
        LoseMenuTrigger = Resources.Load<AudioClip>("LoseMenuTrigger");
        SesEfekti1 = Resources.Load<AudioClip>("SesEfekti1");
        OlmeSesiDaha = Resources.Load<AudioClip>("OlmeSesiDaha");
        HasarAlma = Resources.Load<AudioClip>("HasarAlma");
        SpawnSesi = Resources.Load<AudioClip>("SpawnSesi");
        YurumeSesi = Resources.Load<AudioClip>("YurumeSesi");
        SakizPatlama = Resources.Load<AudioClip>("SakizPatlama");
        BalonPatlat = Resources.Load<AudioClip>("BalonPatlat");
        BalonSisirme = Resources.Load<AudioClip>("BalonSisirme");
        SakizCigneme = Resources.Load<AudioClip>("SakizCigneme");
        END1 = Resources.Load<AudioClip>("END1");
        END2 = Resources.Load<AudioClip>("END2");
        END3 = Resources.Load<AudioClip>("END3");



        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "PickUpSoundEffect":
                audioSrc.PlayOneShot(PickUpSoundEffect);
                break;
            case "LoseMenuTrigger":
                audioSrc.PlayOneShot(LoseMenuTrigger);
                break;
            case "SesEfekti1":
                audioSrc.PlayOneShot(SesEfekti1);
                break;
            case "OlmeSesiDaha":
                audioSrc.PlayOneShot(OlmeSesiDaha);
                break;
            case "HasarAlma":
                audioSrc.PlayOneShot(HasarAlma);
                break;
            case "SpawnSesi":
                audioSrc.PlayOneShot(SpawnSesi);
                break;
            case "YurumeSesi":
                audioSrc.PlayOneShot(YurumeSesi);
                break;
            case "SakizPatlama":
                audioSrc.PlayOneShot(SakizPatlama);
                break;
            case "BalonPatlat":
                audioSrc.PlayOneShot(BalonPatlat);
                break;
            case "BalonSisirme":
                audioSrc.PlayOneShot(BalonSisirme);
                break;
            case "SakizCigneme":
                audioSrc.PlayOneShot(SakizCigneme);
                break;


            case "END1":
                audioSrc.PlayOneShot(END1);
                break;
            case "END2":
                audioSrc.PlayOneShot(END2);
                break;
            case "END3":
                audioSrc.PlayOneShot(END3);
                break;

        }

    }
}
