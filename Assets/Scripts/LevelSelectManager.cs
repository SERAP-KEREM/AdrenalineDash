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


    void Start()
    {

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
    }

    void Update()
    {
        
    }

    public void SceneLoad(int Index)
    {
        ButtonAudio.Play();
        SceneManager.LoadScene(Index);
        Debug.Log(Index);
    }
    public void PreviousButton()
    {
        ButtonAudio.Play();
        SceneManager.LoadScene(0);
    }
}
