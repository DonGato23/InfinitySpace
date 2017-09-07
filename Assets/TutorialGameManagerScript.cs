using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialGameManagerScript : MonoBehaviour
{

    public static int ActionNumber = 0;
    public Text TitleActionText;
    public Text MessageText;
    public GameObject WaveSpawner;
    public GameObject EndTutorialButton;

    private static SoundScript BackgroundSound;


    // Use this for initialization
    void Start () {
        BackgroundSound = FindObjectOfType<SoundScript>();
        InvokeRepeating("IncrementActionNumber", 5f,5f);
    }
	
	// Update is called once per frame
	void Update () {
        if (ActionNumber == 0)
        {
            TitleActionText.text = "Move";
            MessageText.text = "touch wherever the ship moves.";
        }
        if (ActionNumber == 1)
        {
            TitleActionText.text = "Shooting";
            MessageText.text = "The ship fires automatically with certain rate of fire.";
            
        }
        if (ActionNumber == 2) {
            TitleActionText.text = "Enemys";
            MessageText.text = "The enemies will come every certain amount of time, as you advance more will come and stronger.";
            WaveSpawner.SetActive(true);
            CancelInvoke();
            ActionNumber += 1;
        }
        if (ActionNumber == 3)
        {
            Invoke("IncrementActionNumber", 5f);
        }
        if (ActionNumber == 4) {
            EndTutorialButton.SetActive(true);
            TitleActionText.text = "End Tutorial";
            MessageText.text = "touch the 'end tutorial' button to finish. enjoy the game.";
            PlayerPrefs.SetInt("Tutorial", 1);
        }
    }

    void IncrementActionNumber()
    {
        ActionNumber++;
    }

    public void GoMenu() {
        SceneManager.LoadScene(0);
        BackgroundSound.ChangeSound(0);
    }

    public void GoGame() {
        SceneManager.LoadScene(1);
        BackgroundSound.ChangeSound(1);
    }

    public void StopGame() {
        Time.timeScale = 0;
    }


}
