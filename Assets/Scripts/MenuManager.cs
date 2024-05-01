using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject OPTpanel;
    public Button DevamEtBtn;
    void Start()
    {
        if (PlayerPrefs.GetInt("CheckPoint") == 0)
        {
            DevamEtBtn.interactable = false;
        }
        else
        {
            DevamEtBtn.interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGameBTN()
    {
        PlayerPrefs.SetInt("CheckPoint", 0); //silcez sonra
        Time.timeScale = 1;
        SceneManager.LoadScene("game");
    }
    public void PlayGameBTN()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("game");
    }
    public void Optionsçkapa(bool onOFF)
    {
      
        OPTpanel.SetActive(onOFF);
    }
    public void QuitBNTN()
    {

        Application.Quit();
    }
}
