using SerapKerem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    public Button[] Buttons;
    public int Level;
    public Sprite lockedButtonImage;
    
    MemoryManager memoryManager=new MemoryManager();
    public AudioSource ButtonAudio;

    public List<LanguageDataMainObject> languageDataMainObject = new List<LanguageDataMainObject>();
    List<LanguageDataMainObject> languageReadData = new List<LanguageDataMainObject>();
    public TextMeshProUGUI[] TextObjects;
    DataManager dataManager = new DataManager();
    [Header("Loading Datas")]
    public GameObject LoadingPanel;
    public Slider LoadingSlider;
    void Start()
    {
        ButtonAudio.volume = memoryManager.LoadData_Float("MenuFX");
    
       // memoryManager.SaveData_Int("EndLevel", Level);

        int currentLevel = memoryManager.LoadData_Int("EndLevel")-4;
        int Index = 1;
        for (int i = 0; i <Buttons.Length; i++)
        {
            if(i+1<=currentLevel)
            {
                Buttons[i].GetComponentInChildren<TextMeshProUGUI>().text= Index.ToString();
                int CurrentIndex = Index+4;
                Buttons[i].onClick.AddListener(delegate { SceneLoad(CurrentIndex); });
            }
            else
            {
                Buttons[i].GetComponent<Image>().sprite = lockedButtonImage;
              //  Buttons[i].interactable = false;
                Buttons[i].enabled = false;
            }
            Index++;
        }

        dataManager.LanguageLoad();
        languageReadData = dataManager.LanguageExportList();
        languageDataMainObject.Add(languageReadData[2]);

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
    IEnumerator LoadAsync(int SceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);

        LoadingPanel.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingSlider.value = operation.progress;
            yield return null;
        }
    }

    public void SceneLoad(int Index)
    {
        ButtonAudio.Play();
        StartCoroutine(LoadAsync(Index));

    }
    public void PreviousButton()
    {
        ButtonAudio.Play();
        SceneManager.LoadScene(0);
    }
}
