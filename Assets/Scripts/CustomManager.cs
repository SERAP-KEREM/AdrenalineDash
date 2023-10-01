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
    void Start()
    {
        ////////    C:/Users/serap/AppData/LocalLow/DefaultCompany/AdrenalineDash
        // Debug.Log(Application.persistentDataPath);
       // memoryManager.SaveData_Int("Puan", 2000);
        PuanText.text=memoryManager.LoadData_Int("Puan").ToString();
        #region hat start
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
        #endregion hat start

        #region stick start
        memoryManager.SaveData_Int("ActiveStick", -1);
        if (memoryManager.LoadData_Int("ActiveStick") == -1)
        {
            foreach (var item in Sticks)
            {
                item.gameObject.SetActive(false);

            }
            MaterialIndex = -1;
            StickText.text = "No Stick";
        }
        else
        {
            StickIndex = memoryManager.LoadData_Int("ActiveStick");
            Sticks[StickIndex].gameObject.SetActive(true);
        }
        #endregion stick start

        #region thema
        memoryManager.SaveData_Int("ActiveTheme", -1);
        if (memoryManager.LoadData_Int("ActiveTheme") == -1)
        {

            MaterialIndex = -1;
            MaterialText.text = "No Material";
        }
        else
        {
            MaterialIndex = memoryManager.LoadData_Int("ActiveTheme");

            ChangeMaterial(CharacterMaterials[MaterialIndex]);

        }
        #endregion thema
        //   dataManager.Save(itemInformations);

        dataManager.Load();
        itemInformations = dataManager.ExportList();


    }

    public void ItemBuy()
    {
       // Debug.Log(activeItemPanelIndex);
        if (activeItemPanelIndex != -1)
        {
            switch (activeItemPanelIndex)
            {
                case 0:
                    Debug.Log("Chapter no : " + activeItemPanelIndex + "Item Index" + HatIndex+" Item Ad : " + itemInformations[HatIndex].ItemName);
                    break;
                case 1:

                    Debug.Log("Chapter no : " + activeItemPanelIndex + "Item Index" + StickIndex+"Item Ad: " + itemInformations[StickIndex+9].ItemName);
                    break;
                case 2:

                    Debug.Log("Chapter no : " + activeItemPanelIndex + "Item Index" + MaterialIndex+ "Item Ad: " + itemInformations[MaterialIndex+14].ItemName);
                    break;
                default:
                    Debug.Log("error");
                    break;
            }
        }
    }
    public void ItemSave()
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
    public void StickButtons(string _event)
    {
        if (_event == "Next")//next button
        {
            if (StickIndex == -1)
            {
                StickIndex = 0;
                Sticks[StickIndex].SetActive(true);
                StickText.text = itemInformations[StickIndex + 9].ItemName;
            }
            else
            {
                Sticks[StickIndex].SetActive(false);
                StickIndex++;
                Sticks[StickIndex].SetActive(true);
                StickText.text = itemInformations[StickIndex + 9].ItemName;


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

                }
                else
                {
                    StickButtonList[0].interactable = false;
                    StickText.text = "No Stick";


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
        if (_event == "Next")//next button
        {
            if (MaterialIndex == -1)
            {
                MaterialIndex = 0;

                ChangeMaterial(CharacterMaterials[MaterialIndex]);

                MaterialText.text = itemInformations[MaterialIndex].ItemName;
            }
            else
            {
                MaterialIndex++;

                ChangeMaterial(CharacterMaterials[MaterialIndex]);

                MaterialText.text = itemInformations[MaterialIndex + 14].ItemName;


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

                }
                else
                {

                    ChangeMaterial(FirstMaterials);
                    CharacterButtonList[0].interactable = false;
                    MaterialText.text = "No Material";


                }
            }
            else
            {

                ChangeMaterial(FirstMaterials);
                CharacterButtonList[0].interactable = false;
                MaterialText.text = "No Material";

            }
            //--------------
            if (MaterialIndex != CharacterMaterials.Length - 1)
                CharacterButtonList[1].interactable = true;

        }
    }

    public void ItemPanelActive(int Index)
    {
        GeneralPanels[0].SetActive(true);
        activeItemPanelIndex = Index;
        itemPanels[Index].SetActive(true);
        GeneralPanels[1].SetActive(true);
        itemCanvas.SetActive(false);
    }

    public void XButton()
    {
        GeneralPanels[0].SetActive(false);

        itemCanvas.SetActive(true);
        GeneralPanels[1].SetActive(false);
        itemPanels[activeItemPanelIndex].SetActive(false);
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
}
