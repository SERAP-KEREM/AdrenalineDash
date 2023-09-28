using SerapKerem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class CustomManager : MonoBehaviour
{
    public TextMeshProUGUI PuanText;

    [Header("Hats")]
    public TextMeshProUGUI HatText;
    public GameObject[] Hats;
    public Button[] HatButtonList;

    [Header("Sticks")]
    public GameObject[] Sticks;

    [Header("Character Materials")]
    public Material[] CharacterMaterials;

    int HatIndex = -1;

    MemoryManager memoryManager = new MemoryManager();
    DataManager dataManager = new DataManager();

    public List<ItemInformations> itemInformations=new List<ItemInformations>();
    void Start()
    {
        ////////    C:/Users/serap/AppData/LocalLow/DefaultCompany/AdrenalineDash
        // Debug.Log(Application.persistentDataPath);
        memoryManager.SaveData_Int("ActiveHat", -1);

        if (memoryManager.LoadData_Int("ActiveHat") == -1)
        {
            foreach (var item in Hats)
            {
                item.gameObject.SetActive(false);

            }
            HatIndex = -1;
            HatText.text = "No Hat";
        }
        else
        {
            HatIndex = memoryManager.LoadData_Int("ActiveHat");
            Hats[HatIndex].gameObject.SetActive(true);
        }

        dataManager.Save(itemInformations);

        dataManager.Load();
        itemInformations=dataManager.ExportList();

        //Save();
        //Load();
    }

    void Update()
    {

    }
   

    public void HatButtons(string _event)
    {
        if (_event == "Next")//next button
        {
            if (HatIndex == -1)
            {
                HatIndex = 0;
                Hats[HatIndex].SetActive(true);
                HatText.text = itemInformations[HatIndex].ItemName;
            }
            else
            {
                Hats[HatIndex].SetActive(false);
                HatIndex++;
                Hats[HatIndex].SetActive(true);
                HatText.text = itemInformations[HatIndex].ItemName;

            }
            //------------
            if (HatIndex == Hats.Length - 1)
                HatButtonList[1].interactable = false;
            else
                HatButtonList[1].interactable = true;

            if (HatIndex != -1)
                HatButtonList[0].interactable = true;

        }
        else//previous button
        {
            if (HatIndex != -1)
            {
                Hats[HatIndex].SetActive(false);
                HatIndex--;
                if (HatIndex != -1)
                {
                    Hats[HatIndex].SetActive(true);
                    HatButtonList[0].interactable = true;
                    HatText.text = itemInformations[HatIndex].ItemName;
                }
                else
                {
                    HatButtonList[0].interactable = false;
                    HatText.text = "No Hat";


                }
            }
            else
            {
                HatButtonList[0].interactable = false;
                HatText.text = "No Hat";

            }
            //--------------
            if (HatIndex != Hats.Length - 1)
                HatButtonList[1].interactable = true;

        }
    }

}
