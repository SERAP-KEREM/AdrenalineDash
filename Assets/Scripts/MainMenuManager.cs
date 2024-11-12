using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SerapKerem;
using Unity.VisualScripting;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    MemoryManager memoryManager=new MemoryManager();
    DataManager dataManager=new DataManager();  
    public GameObject ExitPanel;

    public List<ItemInformations> default_ItemInformations = new List<ItemInformations>();
    public List<LanguageDataMainObject> default_LanguageData = new List<LanguageDataMainObject>();
    public AudioSource ButtonAudio;

    public List<LanguageDataMainObject> languageDataMainObject = new List<LanguageDataMainObject>();
    List<LanguageDataMainObject> languageReadData = new List<LanguageDataMainObject>();
    public TextMeshProUGUI[] TextObjects;
    public GameObject LoadingPanel;
    public Slider LoadingSlider;

    private void Start()
    {
        memoryManager.ControlAndDefine();
        dataManager.FirstCreateSave(default_ItemInformations, default_LanguageData);
        ButtonAudio.volume = memoryManager.LoadData_Float("MenuFX");
      //  memoryManager.SaveData_String("Language", "TR");

        dataManager.LanguageLoad();
        languageReadData = dataManager.LanguageExportList();
        languageDataMainObject.Add(languageReadData[0]);

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
    public void SceneLoad(int index)
    {
        ButtonAudio.Play();
        SceneManager.LoadScene(index);

    }

    public void Play()
    {
        ButtonAudio.Play();
        StartCoroutine(LoadAsync(memoryManager.LoadData_Int("EndLevel")));
    }

    IEnumerator LoadAsync(int SceneIndex)
    {
        AsyncOperation operation=SceneManager.LoadSceneAsync(SceneIndex);   

        LoadingPanel.SetActive(true);

        while (!operation.isDone)
        {
            float progress=Mathf.Clamp01(operation.progress/0.9f);   
            LoadingSlider.value=operation.progress;
            yield return null;
        }
    }

  
    public void ExitButtonProcess(string _event)
    {

        ButtonAudio.Play();
        if (_event=="YES")
            Application.Quit();
        else if(_event=="EXIT")
            ExitPanel.SetActive(true);
        else
            ExitPanel.SetActive(false);

    }


  
}
