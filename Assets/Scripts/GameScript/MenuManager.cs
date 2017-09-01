using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager> {

    public Text BestScoreText;
    private static SoundScript BackgroundSound;
    private bool Tutorial;
    public GameObject TutorialPanel;

    private void Awake()
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;
        //PlayerPrefs.SetInt("Tutorial", 0);
        BackgroundSound = FindObjectOfType<SoundScript>();
        if (!PlayerPrefs.HasKey("BestScore"))
            PlayerPrefs.SetInt("BestScore", 0);
        BestScoreText.text = "BestScore: " + PlayerPrefs.GetInt("BestScore").ToString();
        
        Tutorial = Convert.ToBoolean(PlayerPrefs.GetInt("Tutorial"));
    }

    public void PlayGame()
    {
        if (Tutorial)
        {
            GoTheGame();
        }
        else {
            TutorialPanel.SetActive(true);
        }
    }

    public void GoTutorial() {
        SceneManager.LoadScene(2);
    }

    public void GoTheGame() {
        SceneManager.LoadScene(1);
        BackgroundSound.ChangeSound(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
