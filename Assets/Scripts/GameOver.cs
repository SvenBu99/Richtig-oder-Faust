using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverTitle;
    public GameObject gameOverPicture;

    public Sprite gewinnSprite;
    public Sprite verlierSprite;

    GameObject testManager;

    private void Awake()
    {
        testManager = GameObject.Find("MusicPlayer");
    }

    public void Start()
    {
        //Spieler gewonnen
        if (PlayerPrefs.GetInt("gewinnerString")==1/*werGewonnen == 1*/)
        {
            gameOverTitle.GetComponent<TextMeshProUGUI>().text = "DU HAST GEWONNEN";
            gameOverPicture.GetComponent<Image>().sprite = gewinnSprite;
            testManager.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("WinMusic");
        }
        else if(PlayerPrefs.GetInt("gewinnerString")==0)
        {
            gameOverTitle.GetComponent<TextMeshProUGUI>().text = "GAME OVER";
            gameOverPicture.GetComponent<Image>().sprite = verlierSprite;
            testManager.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("LoseMusic");
        }
        //Spieler Links bei TwoPlayers
        else if (PlayerPrefs.GetInt("gewinnerString") == 2)
        {
            gameOverTitle.GetComponent<TextMeshProUGUI>().text = "Spieler Links hat gewonnen";
            gameOverPicture.GetComponent<Image>().sprite = gewinnSprite;
            testManager.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("WinMusic");
        }
        //Spieler Rechts bei TwoPlayers
        else if (PlayerPrefs.GetInt("gewinnerString") == 3)
        {
            gameOverTitle.GetComponent<TextMeshProUGUI>().text = "Spieler Rechts hat gewonnen";
            gameOverPicture.GetComponent<Image>().sprite = gewinnSprite;
            testManager.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("WinMusic");
        }
    }
}
