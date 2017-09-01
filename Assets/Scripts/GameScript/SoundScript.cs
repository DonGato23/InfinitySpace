using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundScript : Singleton<SoundScript> {
    public AudioClip[] Clips;
    public AudioSource Audio;

    private void Start()
    {
        SoundScript[] other = FindObjectsOfType<SoundScript>();
        for (int i = 0; i< other.Length; i++) {
            if (Instance != other[i])
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeSound(int newaudio)
    {
        Audio.clip = Clips[newaudio];
        Audio.Play();
    }

    /*private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Audio.clip = Clips[0];
            Audio.Play();
        }
        else
        {
            Audio.clip = Clips[1];
            Audio.Play();
        }
    }*/
}
