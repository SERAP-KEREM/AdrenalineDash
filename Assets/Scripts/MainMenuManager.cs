using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SerapKerem;

public class MainMenuManager : MonoBehaviour
{
    MemoryManager memoryManager=new MemoryManager();
    public GameObject ExitPanel;

    private void Start()
    {
        memoryManager.ControlAndDefine();
    }
    public void SceneLoad(int index)
    {
        SceneManager.LoadScene(index);

    }

    public void Play()
    {
       SceneManager.LoadScene(memoryManager.LoadData_Int("EndLevel"));
        
    }
  
    public void ExitButtonProcess(string _event)
    {
        if(_event=="YES")
            Application.Quit();
        else if(_event=="EXIT")
            ExitPanel.SetActive(true);
        else
            ExitPanel.SetActive(false);

    }
}
