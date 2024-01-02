using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.TextCore.Text;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerapKerem
{
    public class MathematicalOperations
    {
        public void Carpma(int gelenSayi, List<GameObject> Characters, Transform pos, List<GameObject> CreateEffects)
        {

            int DonguSayisi = (GameManager.ActiveCharacterCount * gelenSayi) - GameManager.ActiveCharacterCount;
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

        public void Toplama(int gelenSayi, List<GameObject> Characters, Transform pos, List<GameObject> CreateEffects)
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

        public void Cikarma(int gelenSayi, List<GameObject> Characters, List<GameObject> DestroyEffects)
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
        public void Bolme(int gelenSayi, List<GameObject> Characters, List<GameObject> DestroyEffects)
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
                GameManager.ActiveCharacterCount += 2;
            }
        }



    }

    public class MemoryManager
    {
        public void SaveData_String(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }
        public void SaveData_Int(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();

        }
        public void SaveData_Float(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }


        public string LoadData_String(string key)
        {
            return PlayerPrefs.GetString(key);
        }
        public int LoadData_Int(string key)
        {
            return PlayerPrefs.GetInt(key);
        }
        public float LoadData_Float(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public void ControlAndDefine()
        {
            if (!PlayerPrefs.HasKey("EndLevel"))
            {
                PlayerPrefs.SetInt("EndLevel", 5);
                PlayerPrefs.SetInt("Puan", 0);
                PlayerPrefs.SetInt("ActiveHat", -1);
                PlayerPrefs.SetInt("ActiveStick",-1);
                PlayerPrefs.SetInt("ActiveTheme", -1);
            }
        }
    }

    [Serializable]
    public class ItemInformations
    {
        public int GroupIndex;
        public int ItemIndex;
        public string ItemName;
        public int Puan;
        public bool isBuy;
    }

    public class DataManager
    {
        
        public void Save(List<ItemInformations> itemInformations)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Create(Application.persistentDataPath + "/ItemData.gd");
            binaryFormatter.Serialize(fileStream, itemInformations);
            fileStream.Close();

        }  
       


        List<ItemInformations> itemLoadInformations;
        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemData.gd"))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = File.Open(Application.persistentDataPath + "/ItemData.gd", FileMode.Open);
                itemLoadInformations = (List<ItemInformations>)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();

            }
            else
            {
                Debug.LogError("No File");
            }
        }

        public List<ItemInformations> ExportList()
        {
            return itemLoadInformations;
        }


        public void FirstCreateSave(List<ItemInformations> itemInformations)
        {
            if (!File.Exists(Application.persistentDataPath + "/ItemData.gd"))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = File.Create(Application.persistentDataPath + "/ItemData.gd");
                binaryFormatter.Serialize(fileStream, itemInformations);
                fileStream.Close();

            }


        }
    }
}