using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SerapKerem;
using UnityEngine.TextCore.Text;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int ActiveCharacterCount = 1;
    public List<GameObject> Characters;
    public List<GameObject> CreateEffects;
    public List<GameObject> DestroyEffects;
    public List<GameObject> AdamLekesiEffects;


    [Header("Level Objects")]
    public List<GameObject> Enemies;
    public int EnemyCount;
    public GameObject MainCharacter;
    public bool isFinishGame;
    bool isFinish;

    MathematicalOperations mathematicalOperations = new MathematicalOperations();
    MemoryManager memoryManager = new MemoryManager();

    [Header("----------------Hats")]
    public GameObject[] Hats;

    [Header("----------------Sticks")]
    public GameObject[] Sticks;

    [Header("----------------Character Materials")]
    public Material[] CharacterMaterials;
    public Material FirstMaterials;
    public SkinnedMeshRenderer[] meshRenderer;

    Scene scene;

    [Header("----------------General Data")]
    public AudioSource[] Sources;
    public GameObject[] ProcessPanels;
    public Slider gameVolumeSetting;

    public List<LanguageDataMainObject> languageDataMainObject = new List<LanguageDataMainObject>();
    List<LanguageDataMainObject> languageReadData = new List<LanguageDataMainObject>();
    public TextMeshProUGUI[] TextObjects;
    DataManager dataManager = new DataManager();

    private void Awake()
    {
        Sources[0].volume = memoryManager.LoadData_Float("GameVoice");
        gameVolumeSetting.value = memoryManager.LoadData_Float("GameVoice");
        Sources[1].volume = memoryManager.LoadData_Float("MenuFX");
        Destroy(GameObject.FindWithTag("MenuMusic"));
        CheckTheItems();
    }

    private void Start()
    {
        EnemyMakes();
        Debug.Log(memoryManager.LoadData_Int("Puan"));
        scene = SceneManager.GetActiveScene();

        dataManager.LanguageLoad();
        languageReadData = dataManager.LanguageExportList();
        languageDataMainObject.Add(languageReadData[5]);

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

    public void EnemyMakes()
    {
        for (int i = 0; i < EnemyCount; i++)
        {
            Enemies[i].SetActive(true);
        }
    }

    public void EnemiesAttack()
    {
        foreach (var enemy in Enemies)
        {
            if (enemy.activeInHierarchy)
            {
                enemy.GetComponent<Enemy>().AnimPlay();

            }
        }
        isFinish = true;
        WarEvent();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Instantiate(PrefabCharacter, BirthPoint.transform.position, Quaternion.identity);

            //foreach (var item in Characters)
            //{
            //    if (!item.activeInHierarchy)
            //    {
            //        item.transform.position = BirthPoint.transform.position;
            //        item.SetActive(true);
            //        ActiveCharacterCount++;
            //        break;

            //    }
            //}
        }
    }


    void WarEvent()
    {
        if (isFinish)
        {
            if (ActiveCharacterCount == 1 || EnemyCount == 0)
            {
                isFinishGame = true;
                foreach (var enemy in Enemies)
                {
                    if (enemy.activeInHierarchy)
                    {
                        enemy.GetComponent<Animator>().SetBool("Attack", false);
                    }
                }
                foreach (var character in Characters)
                {
                    if (character.activeInHierarchy)
                    {
                        character.GetComponent<Animator>().SetBool("Attack", false);
                    }
                }
                MainCharacter.GetComponent<Animator>().SetBool("Attack", false);

                if (ActiveCharacterCount < EnemyCount || ActiveCharacterCount == EnemyCount)
                {
                    Debug.Log("Kaybettin");
                }
                else
                {
                    if (ActiveCharacterCount > 5)
                    {
                        if (scene.buildIndex == memoryManager.LoadData_Int("EndLevel"))
                        {
                            memoryManager.SaveData_Int("Puan", memoryManager.LoadData_Int("Puan") + 600);
                            memoryManager.SaveData_Int("EndLevel", memoryManager.LoadData_Int("EndLevel") + 1);
                        }

                    }
                    else
                    {
                        if (scene.buildIndex == memoryManager.LoadData_Int("EndLevel"))
                        {
                            memoryManager.SaveData_Int("Puan", memoryManager.LoadData_Int("Puan") + 200);
                            memoryManager.SaveData_Int("EndLevel", memoryManager.LoadData_Int("EndLevel") + 1);
                        }
                    }
                    Debug.Log("Kazandın");
                }
            }

        }
    }
    public void ManManager(string islemTuru, int numObj, Transform pos)
    {
        switch (islemTuru)
        {
            case "Carpma":
                mathematicalOperations.Carpma(numObj, Characters, pos, CreateEffects);
                break;
            case "Toplama":
                mathematicalOperations.Toplama(numObj, Characters, pos, CreateEffects);
                break;

            case "Cikarma":
                mathematicalOperations.Cikarma(numObj, Characters, DestroyEffects);

                break;
            case "Bolme":
                mathematicalOperations.Bolme(numObj, Characters, DestroyEffects);

                break;
        }
    }

    public void DestroyEffectMakes(Vector3 pos, bool isBalyoz = false, bool CharacterEvent = false)
    {
        foreach (var item in DestroyEffects)
        {
            if (!item.activeInHierarchy)
            {

                item.transform.position = pos;
                item.SetActive(true);
                item.GetComponent<ParticleSystem>().Play();
                item.GetComponent<AudioSource>().Play();

                if (!CharacterEvent)
                    ActiveCharacterCount--;
                else
                    EnemyCount--;
                break;
            }
        }
        if (isBalyoz)
        {
            Vector3 newPos = new Vector3(pos.x, 0.005f, pos.z);

            foreach (var item in AdamLekesiEffects)
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    item.transform.position = newPos;

                    break;
                }
            }
        }

        if (!isFinishGame)
        {
            WarEvent();

        }
    }

    public void CheckTheItems()
    {
        if (memoryManager.LoadData_Int("ActiveHat") != -1)
            Hats[memoryManager.LoadData_Int("ActiveHat")].SetActive(true);
        if (memoryManager.LoadData_Int("ActiveStick") != -1)
            Sticks[memoryManager.LoadData_Int("ActiveStick")].SetActive(true);
        if (memoryManager.LoadData_Int("ActiveTheme") != -1)
            ChangeMaterial(CharacterMaterials[memoryManager.LoadData_Int("ActiveTheme")]);
        else
            ChangeMaterial(FirstMaterials);

    }

    public void ExitButtonProcess(string _event)
    {

        Sources[1].Play();
        Time.timeScale = 0;
        if (_event == "Stop")
        {
            ProcessPanels[0].SetActive(true);

        }
        else if(_event =="Continue")
        {
            ProcessPanels[0].SetActive(false);
            Time.timeScale = 1;
        }
        else if(_event =="Replay")
        {
            SceneManager.LoadScene(scene.buildIndex);
            Time.timeScale = 1;

        }
        else if(_event =="Home")
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;

        }


        //else if (_event == "EXIT")
        //  ExitPanel.SetActive(true);
        // else
        //   ExitPanel.SetActive(false);

    }
    public void Settings(string _event)
    {
        if(_event =="setting")
        {
            ProcessPanels[1].SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            ProcessPanels[1].SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void AdjustVolume()
    {
        memoryManager.SaveData_Float("GameVoice", gameVolumeSetting.value);
        Sources[0].volume = gameVolumeSetting.value;
       
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
