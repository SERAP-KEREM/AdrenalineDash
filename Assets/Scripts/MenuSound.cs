using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{
    private static GameObject instance;

    public AudioSource sound;

    private void Start()
    {
        //sound=GetComponent<AudioSource>();

        sound.volume = PlayerPrefs.GetFloat("MenuVoice");
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        sound.volume = PlayerPrefs.GetFloat("MenuVoice");
    }
}
