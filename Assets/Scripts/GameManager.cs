using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Olcay;

public class GameManager : MonoBehaviour
{
    public GameObject DestinationPoint;
    [SerializeField] public static int ActiveCharacterCount = 1;

    public List<GameObject> Characters;
    public List<GameObject> CreateEffects;
    public List<GameObject> DestroyEffects;
    public List<GameObject> AdamLekesiEffects;

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

    public void ManManager(string islemTuru, int numObj, Transform pos)
    {
        switch (islemTuru)
        {
            case "Carpma":
                MathematicalOperations.Carpma(numObj, Characters, pos,CreateEffects);
                break;
            case "Toplama":
                MathematicalOperations.Toplama(numObj, Characters, pos, CreateEffects);
                break;

            case "Cikarma":
                MathematicalOperations.Cikarma(numObj, Characters,DestroyEffects);

                break;
            case "Bolme":
                MathematicalOperations.Bolme(numObj, Characters,DestroyEffects);

                break;
        }
    }

    public void DestroyEffectMakes(Vector3 pos, bool isBalyoz=false)
    {
        foreach (var item in DestroyEffects)
        {
            if (!item.activeInHierarchy)
            {

                item.transform.position = pos;
                item.SetActive(true);
                item.GetComponent<ParticleSystem>().Play();
                GameManager.ActiveCharacterCount--;

                break;
            }
        }
        if(isBalyoz)
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
    }  
  
}


