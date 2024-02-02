using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{

    public AudioSource ButtonVoice;
    public Slider MenuVoice;
    public Slider MenuFX;
    public Slider GameVoice;
    
    void Start()
    {
    //    PlayerPrefs.SetFloat("MenuVoice", 1);
    //    PlayerPrefs.SetFloat("MenuFX", 1);
    //    PlayerPrefs.SetFloat("GameVoice", 1);
    }

    void Update()
    {

    }
    public void PreviousButton()
    {
        ButtonVoice.Play();
        SceneManager.LoadScene(0);
    }

    public void ChangeLanguage()
    {
        ButtonVoice.Play();
    }
   
}
