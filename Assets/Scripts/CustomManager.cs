using SerapKerem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.TextCore.Text;
using static UnityEditor.Progress;
using UnityEngine.SceneManagement;

public class CustomManager : MonoBehaviour
{
    public TextMeshProUGUI PuanText;

    public GameObject[] itemPanels;
    public GameObject itemCanvas;
    public GameObject[] GeneralPanels;
    public Button[] OperationButtons;
    public TextMeshProUGUI BuyButtonText;

    int activeItemPanelIndex;


    [Header("----------------Hats")]
    public TextMeshProUGUI HatText;
    public GameObject[] Hats;
    public Button[] HatButtonList;

    [Header("----------------Sticks")]
    public TextMeshProUGUI StickText;
    public GameObject[] Sticks;
    public Button[] StickButtonList;


    [Header("----------------Character Materials")]
    public TextMeshProUGUI MaterialText;
    public Material[] CharacterMaterials;
    public Material FirstMaterials;
    public Button[] CharacterButtonList;
    public SkinnedMeshRenderer[] meshRenderer;


    int HatIndex = -1;
    int StickIndex = -1;
    int MaterialIndex = -1;

    MemoryManager memoryManager = new MemoryManager();
    DataManager dataManager = new DataManager();

    [Header("---------------GENERATION OBJECTS")]
    public List<ItemInformations> itemInformations = new List<ItemInformations>();

    public Animator SavedInformation;

    public AudioSource[] Sounds;

    void Start()
    {
        ////////    C:/Users/serap/AppData/LocalLow/DefaultCompany/AdrenalineDash
        // Debug.Log(Application.persistentDataPath);

        memoryManager.SaveData_Int("Puan", 6300);
        PuanText.text = memoryManager.LoadData_Int("Puan").ToString();


        //   dataManager.Save(itemInformations);

        dataManager.Load();
        itemInformations = dataManager.ExportList();

        CheckTheStatus(0, true);
        CheckTheStatus(1, true);
        CheckTheStatus(2, true);

        foreach (var item in Sounds)
        {
            item.volume = memoryManager.LoadData_Float("MenuFX");
        }
    }

    void CheckTheStatus(int chapter, bool operation = false)
    {
        if (chapter == 0)
        {
            #region hat start
            if (memoryManager.LoadData_Int("ActiveHat") == -1)
            {
                foreach (var item in Hats)
                {
                    item.gameObject.SetActive(false);

                }

                OperationButtons[0].interactable = false;
                OperationButtons[1].interactable = false;
                BuyButtonText.text = "Buy";
                if (!operation)
                {

                    HatIndex = -1;
                    HatText.text = "No Hat";
                }

            }
            else
            {
                foreach (var item in Hats)
                {
                    item.gameObject.SetActive(false);

                }
                HatIndex = memoryManager.LoadData_Int("ActiveHat");
                Hats[HatIndex].gameObject.SetActive(true);

                HatText.text = itemInformations[HatIndex].ItemName;
                OperationButtons[0].interactable = false;
                OperationButtons[1].interactable = true;
                BuyButtonText.text = "Buy";
            }
            #endregion hat start
        }
        else if (chapter == 1)
        {
            #region stick start
            if (memoryManager.LoadData_Int("ActiveStick") == -1)
            {
                foreach (var item in Sticks)
                {
                    item.gameObject.SetActive(false);

                }

                OperationButtons[0].interactable = false;
                OperationButtons[1].interactable = false;
                if (!operation)
                {
                    StickIndex = -1;
                    StickText.text = "No Stick";
                }
            }
            else
            {
                foreach (var item in Sticks)
                {
                    item.gameObject.SetActive(false);

                }
                StickIndex = memoryManager.LoadData_Int("ActiveStick");
                Sticks[StickIndex].gameObject.SetActive(true);

                StickText.text = itemInformations[StickIndex + 9].ItemName;
                OperationButtons[0].interactable = false;
                OperationButtons[1].interactable = true;
                BuyButtonText.text = "Buy";
            }
            #endregion stick start
        }
        else if (chapter == 2)
        {
            #region thema
            if (memoryManager.LoadData_Int("ActiveTheme") == -1)
            {
                if (!operation)
                {
                    MaterialIndex = -1;
                    MaterialText.text = "No Material";
                    OperationButtons[0].interactable = false;
                    OperationButtons[1].interactable = false;
                }
                else
                {
                    ChangeMaterial(FirstMaterials);
                }

            }
            else
            {
                MaterialIndex = memoryManager.LoadData_Int("ActiveTheme");

                ChangeMaterial(CharacterMaterials[MaterialIndex]);

                MaterialText.text = itemInformations[MaterialIndex + 14].ItemName;
                OperationButtons[0].interactable = false;
                OperationButtons[1].interactable = true;
                BuyButtonText.text = "Buy";

            }
            #endregion thema
        }




    }

    public void ItemBuy()
    {
        Sounds[1].Play();

        // Debug.Log(activeItemPanelIndex);
        if (activeItemPanelIndex != -1)
        {
            switch (activeItemPanelIndex)
            {
                case 0:
                    Debug.Log("Chapter no : " + activeItemPanelIndex + "Item Index" + HatIndex + " Item Ad : " + itemInformations[HatIndex].ItemName);
                    BuyResult(HatIndex);
                    break;
                case 1:
                    Debug.Log("Chapter no : " + activeItemPanelIndex + "Item Index" + StickIndex + "Item Ad: " + itemInformations[StickIndex + 9].ItemName);
                    int index = StickIndex + 9;
                    BuyResult(index);
                    break;
                case 2:
                    Debug.Log("Chapter no : " + activeItemPanelIndex + "Item Index" + MaterialIndex + "Item Ad: " + itemInformations[MaterialIndex + 14].ItemName);
                    int index2 = MaterialIndex + 14;
                    BuyResult(index2);
                    break;
                default:
                    Debug.Log("error");
                    break;
            }
        }
    }

    public void ItemSave()
    {
        Sounds[2].Play();

        if (activeItemPanelIndex != -1)
        {
            switch (activeItemPanelIndex)
            {
                case 0:
                    SaveResult("ActiveHat", HatIndex);
                    break;
                case 1:
                    SaveResult("ActiveStick", StickIndex);

                    break;
                case 2:
                    SaveResult("ActiveTheme", MaterialIndex);
                    break;
                default:
                    Debug.Log("error");
                    break;
            }
        }
    }

    public void HatButtons(string _event)
    {
        Sounds[0].Play();

        if (_event == "Next")//next button
        {
            if (HatIndex == -1)
            {
                HatIndex = 0;
                Hats[HatIndex].SetActive(true);
                HatText.text = itemInformations[HatIndex].ItemName;

                if (!itemInformations[HatIndex].isBuy)
                {
                    BuyButtonText.text = itemInformations[HatIndex].Puan + " - Buy";
                    OperationButtons[1].interactable = false;

                    if (memoryManager.LoadData_Int("Puan") < itemInformations[HatIndex].Puan)
                        OperationButtons[0].interactable = false;
                    else
                        OperationButtons[0].interactable = true;
                }
                else
                {
                    BuyButtonText.text = "Buy";
                    OperationButtons[0].interactable = false;
                    OperationButtons[1].interactable = true;
                }
            }
            else
            {
                Hats[HatIndex].SetActive(false);
                HatIndex++;
                Hats[HatIndex].SetActive(true);
                HatText.text = itemInformations[HatIndex].ItemName;

                if (!itemInformations[HatIndex].isBuy)
                {
                    BuyButtonText.text = itemInformations[HatIndex].Puan + " - Buy";
                    OperationButtons[1].interactable = false;

                    if (memoryManager.LoadData_Int("Puan") < itemInformations[HatIndex].Puan)
                        OperationButtons[0].interactable = false;
                    else
                        OperationButtons[0].interactable = true;
                }
                else
                {
                    BuyButtonText.text = "Buy";
                    OperationButtons[0].interactable = false;
                    OperationButtons[1].interactable = true;
                }

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

                    if (!itemInformations[HatIndex].isBuy)
                    {
                        BuyButtonText.text = itemInformations[HatIndex].Puan + " - Buy";
                        OperationButtons[1].interactable = false;

                        if (memoryManager.LoadData_Int("Puan") < itemInformations[HatIndex].Puan)
                            OperationButtons[0].interactable = false;
                        else
                            OperationButtons[0].interactable = true;
                    }
                    else
                    {
                        BuyButtonText.text = "Buy";
                        OperationButtons[0].interactable = false;
                        OperationButtons[1].interactable = true;
                    }

                }
                else
                {
                    HatButtonList[0].interactable = false;
                    HatText.text = "No Hat";
                    BuyButtonText.text = "Buy";
                    OperationButtons[0].interactable = false;
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
    public void StickButtons(string _event)
    {
        Sounds[0].Play();

        if (_event == "Next")//next button
        {
            if (StickIndex == -1)
            {
                StickIndex = 0;
                Sticks[StickIndex].SetActive(true);
                StickText.text = itemInformations[StickIndex + 9].ItemName;

                if (!itemInformations[StickIndex + 9].isBuy)
                {
                    BuyButtonText.text = itemInformations[StickIndex + 9].Puan + " - Buy";
                    OperationButtons[1].interactable = false;

                    if (memoryManager.LoadData_Int("Puan") < itemInformations[StickIndex + 9].Puan)
                        OperationButtons[0].interactable = false;
                    else
                        OperationButtons[0].interactable = true;
                }
                else
                {
                    BuyButtonText.text = "Buy";
                    OperationButtons[0].interactable = false;
                    OperationButtons[1].interactable = true;
                }
            }
            else
            {
                Sticks[StickIndex].SetActive(false);
                StickIndex++;
                Sticks[StickIndex].SetActive(true);
                StickText.text = itemInformations[StickIndex + 9].ItemName;


                if (!itemInformations[StickIndex + 9].isBuy)
                {
                    BuyButtonText.text = itemInformations[StickIndex + 9].Puan + " - Buy";
                    OperationButtons[1].interactable = false;

                    if (memoryManager.LoadData_Int("Puan") < itemInformations[StickIndex + 9].Puan)
                        OperationButtons[0].interactable = false;
                    else
                        OperationButtons[0].interactable = true;
                }
                else
                {
                    BuyButtonText.text = "Buy";
                    OperationButtons[0].interactable = false;
                    OperationButtons[1].interactable = true;

                }

            }
            //------------
            if (StickIndex == Sticks.Length - 1)
                StickButtonList[1].interactable = false;
            else
                StickButtonList[1].interactable = true;

            if (StickIndex != -1)
                StickButtonList[0].interactable = true;

        }
        else//previous button
        {
            if (StickIndex != -1)
            {
                Sticks[StickIndex].SetActive(false);
                StickIndex--;
                if (StickIndex != -1)
                {
                    Sticks[StickIndex].SetActive(true);
                    StickButtonList[0].interactable = true;
                    StickText.text = itemInformations[StickIndex + 9].ItemName;

                    //*
                    if (!itemInformations[StickIndex + 9].isBuy)
                    {
                        BuyButtonText.text = itemInformations[StickIndex + 9].Puan + " - Buy";
                        OperationButtons[1].interactable = false;

                        if (memoryManager.LoadData_Int("Puan") < itemInformations[StickIndex + 9].Puan)
                            OperationButtons[0].interactable = false;
                        else
                            OperationButtons[0].interactable = true;
                    }
                    else
                    {
                        BuyButtonText.text = "Buy";
                        OperationButtons[0].interactable = false;
                        OperationButtons[1].interactable = true;
                    }
                    //*
                }
                else
                {
                    StickButtonList[0].interactable = false;
                    StickText.text = "No Stick";
                    BuyButtonText.text = "Buy";
                    OperationButtons[0].interactable = false;


                }
            }
            else
            {
                StickButtonList[0].interactable = false;
                StickText.text = "No Stick";

            }
            //--------------
            if (StickIndex != Sticks.Length - 1)
                StickButtonList[1].interactable = true;

        }
    }

    public void MaterialButtons(string _event)
    {
        Sounds[0].Play();


        if (_event == "Next")//next button
        {
            if (MaterialIndex == -1)
            {
                MaterialIndex = 0;

                ChangeMaterial(CharacterMaterials[MaterialIndex]);

                MaterialText.text = itemInformations[MaterialIndex + 14].ItemName;

                //*
                if (!itemInformations[MaterialIndex + 14].isBuy)
                {
                    BuyButtonText.text = itemInformations[MaterialIndex + 14].Puan + " - Buy";
                    OperationButtons[1].interactable = false;

                    if (memoryManager.LoadData_Int("Puan") < itemInformations[MaterialIndex + 14].Puan)
                        OperationButtons[0].interactable = false;
                    else
                        OperationButtons[0].interactable = true;
                }
                else
                {
                    BuyButtonText.text = "Buy";
                    OperationButtons[0].interactable = false;
                    OperationButtons[1].interactable = true;
                }
                //*
            }
            else
            {
                MaterialIndex++;

                ChangeMaterial(CharacterMaterials[MaterialIndex]);

                MaterialText.text = itemInformations[MaterialIndex + 14].ItemName;
                //*
                if (!itemInformations[MaterialIndex + 14].isBuy)
                {
                    BuyButtonText.text = itemInformations[MaterialIndex + 14].Puan + " - Buy";
                    OperationButtons[1].interactable = false;

                    if (memoryManager.LoadData_Int("Puan") < itemInformations[MaterialIndex + 14].Puan)
                        OperationButtons[0].interactable = false;
                    else
                        OperationButtons[0].interactable = true;
                }
                else
                {
                    BuyButtonText.text = "Buy";
                    OperationButtons[0].interactable = false;
                    OperationButtons[1].interactable = true;
                }
                //*

            }
            //------------
            if (MaterialIndex == CharacterMaterials.Length - 1)
                CharacterButtonList[1].interactable = false;
            else
                CharacterButtonList[1].interactable = true;

            if (MaterialIndex != -1)
                CharacterButtonList[0].interactable = true;

        }
        else//previous button
        {
            if (MaterialIndex != -1)
            {

                MaterialIndex--;
                if (MaterialIndex != -1)
                {

                    ChangeMaterial(CharacterMaterials[MaterialIndex]);

                    CharacterButtonList[0].interactable = true;
                    MaterialText.text = itemInformations[MaterialIndex + 14].ItemName;
                    //*
                    if (!itemInformations[MaterialIndex + 14].isBuy)
                    {
                        BuyButtonText.text = itemInformations[MaterialIndex + 14].Puan + " - Buy";
                        OperationButtons[1].interactable = false;

                        if (memoryManager.LoadData_Int("Puan") < itemInformations[MaterialIndex + 14].Puan)
                            OperationButtons[0].interactable = false;
                        else
                            OperationButtons[0].interactable = true;
                    }
                    else
                    {
                        BuyButtonText.text = "Buy";
                        OperationButtons[0].interactable = false;
                        OperationButtons[1].interactable = true;
                    }
                    //*
                }
                else
                {

                    ChangeMaterial(FirstMaterials);
                    CharacterButtonList[0].interactable = false;
                    MaterialText.text = "No Material";
                    BuyButtonText.text = "Buy";
                    OperationButtons[0].interactable = false;
                }
            }
            else
            {

                ChangeMaterial(FirstMaterials);
                CharacterButtonList[0].interactable = false;
                MaterialText.text = "No Material";
                BuyButtonText.text = "Buy";
                OperationButtons[0].interactable = false;

            }
            //--------------
            if (MaterialIndex != CharacterMaterials.Length - 1)
                CharacterButtonList[1].interactable = true;

        }
    }

    public void ItemPanelActive(int Index)
    {
        Sounds[0].Play();
        CheckTheStatus(Index);
        GeneralPanels[0].SetActive(true);
        activeItemPanelIndex = Index;
        itemPanels[Index].SetActive(true);
        GeneralPanels[1].SetActive(true);
        itemCanvas.SetActive(false);
    }

    public void XButton()
    {
        Sounds[0].Play();
        BuyButtonText.text = "Buy";

        GeneralPanels[0].SetActive(false);

        itemCanvas.SetActive(true);
        GeneralPanels[1].SetActive(false);
        itemPanels[activeItemPanelIndex].SetActive(false);
        CheckTheStatus(activeItemPanelIndex, true);

        activeItemPanelIndex = -1;

    }
    void ChangeMaterial(Material index)
    {
        Material[] mats = meshRenderer[0].materials;
        mats[0] = index;
        meshRenderer[0].materials = mats;
        meshRenderer[1].materials = mats;
        meshRenderer[2].materials = mats;
    }

    public void ReturnMainMenu()
    {
        Sounds[0].Play();
        dataManager.Save(itemInformations);
        SceneManager.LoadScene(0);

    }

    //---------------------------
    void BuyResult(int index)
    {
        //  Debug.Log("Chapter no : " + activeItemPanelIndex + "Item Index" + HatIndex + " Item Ad : " + itemInformations[HatIndex].ItemName);
        itemInformations[index].isBuy = true;
        memoryManager.SaveData_Int("Puan", memoryManager.LoadData_Int("Puan") - itemInformations[index].Puan);
        BuyButtonText.text = "Buy";
        OperationButtons[0].interactable = false;
        OperationButtons[1].interactable = true;
        PuanText.text = memoryManager.LoadData_Int("Puan").ToString();
    }

    void SaveResult(string key, int index)
    {
        memoryManager.SaveData_Int(key, index);
        OperationButtons[1].interactable = false;
        if (!SavedInformation.GetBool("ok"))
            SavedInformation.SetBool("ok", true);
        // Debug.Log("Chapter no : " + activeItemPanelIndex + "Item Index" + HatIndex + " Item Ad : " + itemInformations[HatIndex].ItemName);
    }

}
