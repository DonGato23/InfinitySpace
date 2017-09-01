using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : Singleton<StageManager> {

    public Text LivesText;
    public Text ScoreText;
    public GameObject GameOverText;
    public GameObject ReadyText;
    public GameObject WaveSpawnerObject;
    public GameObject PausePanel;
    private static SoundScript BackgroundSound;
    //private static float _finalPosAudio=0;

    public static bool GameOver=false;
    public static bool PlayGame = false;

    private void Awake()
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;
        BackgroundSound = FindObjectOfType<SoundScript>();
        //BackgroundSound.time=_finalPosAudio;
        //DontDestroyOnLoad(BackgroundSound);
        if (PlayerControl.Lives == 0)
        {
            EndGame();
            GameOverText.SetActive(true);
        }
        else if (!PlayGame)
        {
            ReadyText.SetActive(true);
            Invoke("GoText", 1.5f);
        }
        if (PlayGame && !GameOver)
            WaveSpawnerObject.SetActive(true);
    }

    private void Update()
    {
        LivesText.text = PlayerControl.Lives.ToString();
        ScoreText.text = PlayerControl._currentScore.ToString();
        if (PlayGame & !GameOver)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Time.timeScale = 0;
                PausePanel.SetActive(true);
            }
        }
    }

    public static void RestLive()
    {
        PlayerControl.Lives--;
        //StageManager._finalPosAudio = StageManager.BackgroundSound.time;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    void EndGame()
    {
        GameOver = true;
        if (PlayerPrefs.GetInt("BestScore")<(int)PlayerControl._currentScore)
            PlayerPrefs.SetInt("BestScore",(int)PlayerControl._currentScore);
    }

    void GoText()
    {
        ReadyText.GetComponent<Text>().text = "GO";
        Invoke("PlayGameMethod", 1f);
    }

    void PlayGameMethod()
    {
        PlayGame = true;
        ReadyText.SetActive(false);
        WaveSpawnerObject.SetActive(true);
    }

    public void Unpause()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerControl.Lives = 3;
        PlayerControl._currentScore = 0;
        WaveSpawner.WaveIndex = -1; 
        PlayGame = false;
        GameOver = false;
        //StageManager._finalPosAudio = 0;
    }

    public void GoMenu()
    {
        GameOver = false;
        PlayGame = false;
        PlayerControl.Lives = 3;
        PlayerControl._currentScore = 0;
        WaveSpawner.WaveIndex = -1;
        Unpause();
        //StageManager._finalPosAudio = 0;
        SceneManager.LoadScene(0);
        BackgroundSound.ChangeSound(0);
    }
}
