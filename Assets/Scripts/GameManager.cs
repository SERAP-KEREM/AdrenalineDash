using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Olcay;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public GameObject DestinationPoint;
    [SerializeField] public static int ActiveCharacterCount = 1;

    public List<GameObject> Characters;
    public List<GameObject> CreateEffects;
    public List<GameObject> DestroyEffects;
    public List<GameObject> AdamLekesiEffects;


    [Header("Level Objects")]
    public List<GameObject> Enemies;
    public int EnemyCount;
    public GameObject MainCharacter;
    public bool isFinishGame;

    private void Start()
    {
        EnemyMakes();
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
        if(ActiveCharacterCount==1|| EnemyCount==0)
        {
            isFinishGame = true;
            foreach(var enemy in Enemies)
            {
                if(enemy.activeInHierarchy)
                {
                    enemy.GetComponent<Animator>().SetBool("Attack", false);
                }
            }
            foreach(var character in Characters)
            {
                if(character.activeInHierarchy)
                {
                    character.GetComponent<Animator>().SetBool("Attack", false);
                }
            }
            MainCharacter.GetComponent<Animator>().SetBool("Attack", false);

            if (ActiveCharacterCount < EnemyCount || ActiveCharacterCount==EnemyCount)
            {
                Debug.Log("Kaybettin");
            }
            else
            {
                Debug.Log("Kazandın");

            }
        }

    }
    public void ManManager(string islemTuru, int numObj, Transform pos)
    {
        switch (islemTuru)
        {
            case "Carpma":
                MathematicalOperations.Carpma(numObj, Characters, pos, CreateEffects);
                break;
            case "Toplama":
                MathematicalOperations.Toplama(numObj, Characters, pos, CreateEffects);
                break;

            case "Cikarma":
                MathematicalOperations.Cikarma(numObj, Characters, DestroyEffects);

                break;
            case "Bolme":
                MathematicalOperations.Bolme(numObj, Characters, DestroyEffects);

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


