using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bayrak : MonoBehaviour
{
    public GameObject Bayrakobje;
    public int CHNUM;
    void Start()
    {
        if (CHNUM <= PlayerPrefs.GetInt("CheckPoint"))
        {
            Bayrakobje.SetActive(true);


        }

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void BayrakAç()
    {
        Bayrakobje.SetActive(true);

    }
 
}
