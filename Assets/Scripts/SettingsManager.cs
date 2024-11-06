using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SerapKerem;
using System.Buffers;
using TMPro;

public class SettingsManager : MonoBehaviour
{

    public AudioSource ButtonVoice;
    public Slider MenuVoice;
    public Slider MenuFX;
    public Slider GameVoice;
    MemoryManager memoryManager = new MemoryManager();
    public List<LanguageDataMainObject> languageDataMainObject = new List<LanguageDataMainObject>();
    List<LanguageDataMainObject> languageReadData = new List<LanguageDataMainObject>();
    public TextMeshProUGUI[] TextObjects;
    DataManager dataManager = new DataManager();

    [Header("Language Options Objects")]
    public TextMeshProUGUI[] LanguageText;
    public Button[] LanguageButtons;
    int activeLanguageIndex=0;
 

    void Start()
    {
        ButtonVoice.volume = memoryManager.LoadData_Float("MenuFX");

        MenuVoice.value = memoryManager.LoadData_Float("MenuVoice");
        MenuFX.value = memoryManager.LoadData_Float("MenuFX");
        GameVoice.value = memoryManager.LoadData_Float("GameVoice");
        dataManager.LanguageLoad();
        languageReadData = dataManager.LanguageExportList();
        languageDataMainObject.Add(languageReadData[4]);

        LanguageChoiceManagement();

    }
    public void LanguageChoiceManagement()
    {
        if (memoryManager.LoadData_String("Language") == "TR")
        {
            for (int i = 0; i < TextObjects.Length; i++)
            {
                TextObjects[i].text = languageDataMainObject[0].languageData_TR[i]._text;
            }
        }
        else
        {
            for (int i = 0; i < TextObjects.Length; i++)
            {
                TextObjects[i].text = languageDataMainObject[0].languageData_EN[i]._text;
            }
        }


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

    public void ChangeLanguage(string butonText)
    {

        if (butonText == "next")
        {

        }
        else
        {

        }

        ButtonVoice.Play();
    }
   
}
