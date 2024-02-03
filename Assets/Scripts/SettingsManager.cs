using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SerapKerem;
using System.Buffers;

public class SettingsManager : MonoBehaviour
{

    public AudioSource ButtonVoice;
    public Slider MenuVoice;
    public Slider MenuFX;
    public Slider GameVoice;
    MemoryManager memoryManager = new MemoryManager();
    
    void Start()
    {
        ButtonVoice.volume = memoryManager.LoadData_Float("MenuFX");

        MenuVoice.value = memoryManager.LoadData_Float("MenuVoice");
        MenuFX.value = memoryManager.LoadData_Float("MenuFX");
        GameVoice.value = memoryManager.LoadData_Float("GameVoice");

    }

    void Update()
    {

    }

    public void AdjustSound(string sliderName)
    {
        switch (sliderName)
        {
            case "MenuVoice":
                memoryManager.SaveData_Float("MenuVoice", MenuVoice.value);
                break;
            case "MenuFX":
                memoryManager.SaveData_Float("MenuFX", MenuFX.value);
                break;
            case "GameVoice":
                 memoryManager.SaveData_Float("GameVoice", GameVoice.value);
                break;  
        }

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
