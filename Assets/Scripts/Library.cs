using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.TextCore.Text;

namespace Olcay
{
    public class MathematicalOperations : MonoBehaviour
    {
        public static void Carpma(int gelenSayi,List<GameObject> Characters,Transform pos, List<GameObject> CreateEffects)
        {

            int DonguSayisi=(GameManager.ActiveCharacterCount*gelenSayi)-GameManager.ActiveCharacterCount;
            int num = 0;

            foreach (var item in Characters)
            {
                if (num < DonguSayisi)
                {
                    if (!item.activeInHierarchy)
                    {

                        foreach (var item2 in CreateEffects)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.SetActive(true);
                                item2.transform.position = pos.position;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();

                                break;

                            }
                        }
                        item.transform.position = pos.position;
                        item.SetActive(true);
                        num++;
                    }
                }
                else
                {
                    num = 0;
                    break;
                }

            }
            GameManager.ActiveCharacterCount *= gelenSayi;
        }

        public static void Toplama(int gelenSayi, List<GameObject> Characters, Transform pos, List<GameObject> CreateEffects)
        {
            int num2 = 0;

            foreach (var item in Characters)
            {
                if (num2 < gelenSayi)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in CreateEffects)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.SetActive(true);
                                item2.transform.position = pos.position;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();

                                break;

                            }
                        }
                        item.transform.position = pos.position;
                        item.SetActive(true);
                        num2++;
                    }
                }
                else
                {
                    num2 = 0;
                    break;
                }

            }
           GameManager.ActiveCharacterCount += 3;
        }

        public static void Cikarma(int gelenSayi, List<GameObject> Characters , List<GameObject> DestroyEffects)
        {
            if (GameManager.ActiveCharacterCount < gelenSayi)
            {
                foreach (var item in Characters)
                {

                    foreach (var item2 in DestroyEffects)
                    {
                        if (!item2.activeInHierarchy)
                        {
                            Vector3 newPos = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);

                            item2.SetActive(true);
                            item2.transform.position = newPos;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();

                            break;

                        }
                    }
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.ActiveCharacterCount = 1;
            }

            else
            {
                int num3 = 0;

                foreach (var item in Characters)
                {
                    if (num3 != gelenSayi)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in DestroyEffects)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    Vector3 newPos = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);

                                    item2.SetActive(true);
                                    item2.transform.position = newPos;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    item2.GetComponent<AudioSource>().Play();

                                    break;

                                }
                            }
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            num3++;
                        }
                    }
                    else
                    {
                        num3 = 0;
                        break;
                    }

                }
            }
           GameManager.ActiveCharacterCount -= 4;
        }
        public static void Bolme(int gelenSayi, List<GameObject> Characters, List<GameObject> DestroyEffects)
        {

            if (GameManager.ActiveCharacterCount <= gelenSayi)
            {
                foreach (var item in Characters)
                {
                    foreach (var item2 in DestroyEffects)
                    {
                        if (!item2.activeInHierarchy)
                        {
                            Vector3 newPos = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);

                            item2.SetActive(true);
                            item2.transform.position = newPos;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();

                            break;

                        }
                    }
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
               GameManager.ActiveCharacterCount = 1;
            }

            else
            {
                int bolen = GameManager.ActiveCharacterCount / gelenSayi;
                int num3 = 0;

                foreach (var item in Characters)
                {
                    if (num3 != bolen)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in DestroyEffects)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    Vector3 newPos = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);

                                    item2.SetActive(true);
                                    item2.transform.position = newPos;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    item2.GetComponent<AudioSource>().Play();

                                    break;

                                }
                            }
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            num3++;
                        }
                    }
                    else
                    {
                        num3 = 0;
                        break;
                    }

                }
            }
            if (GameManager.ActiveCharacterCount % gelenSayi == 0)
            {
                GameManager.ActiveCharacterCount /= gelenSayi;

            }
            else if (GameManager.ActiveCharacterCount % gelenSayi == 1)
            {
                GameManager.ActiveCharacterCount /= gelenSayi;
                GameManager.ActiveCharacterCount++;

            }
            else
            {
                GameManager.ActiveCharacterCount /= gelenSayi;
                GameManager.ActiveCharacterCount+=2;
            }
        }

    }
}