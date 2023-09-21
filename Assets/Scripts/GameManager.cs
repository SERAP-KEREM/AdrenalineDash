using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerapKerem;
using UnityEngine.TextCore.Text;

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

    MathematicalOperations mathematicalOperations =new MathematicalOperations();
    MemoryManager memoryManager = new MemoryManager();
    private void Start()
    {
        EnemyMakes();
       Debug.Log(memoryManager.LoadData_Int("Puan"));
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
        foreach (var enemy in Enemies) {
            if (enemy.activeInHierarchy)
            {
                enemy.GetComponent<Enemy>().AnimPlay();

            }
        }
        isFinish= true;
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
        if(isFinish)
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
                    if(ActiveCharacterCount>5)
                        memoryManager.SaveData_Int("Puan", memoryManager.LoadData_Int("Puan")+600);
                    else
                        memoryManager.SaveData_Int("Puan", memoryManager.LoadData_Int("Puan") + 200);


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

    public void DestroyEffectMakes(Vector3 pos, bool isBalyoz = false,bool CharacterEvent=false)
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

        if(!isFinishGame)
        {
            WarEvent();

        }
    }

}


