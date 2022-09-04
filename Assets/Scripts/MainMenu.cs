using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using GoogleMobileAds.Api;
//using GoogleMobileAds.Common;


public class MainMenu : MonoBehaviour
{
    GameObject testManager;

    private void Awake()
    {
        testManager = GameObject.Find("MusicPlayer");
        if (SceneManager.GetActiveScene().buildIndex != 4 && SceneManager.GetActiveScene().buildIndex != 2)
        {
           // ShowBannerAd();
        }
        Time.timeScale = 1;
    }
    public void PlayGame()
    {
        //AdmobAds.instance.ShowInterstitialAd();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void OnePlayer()
    {

        //AdmobAds.instance.DestroyBannerAd();
        testManager.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("FightMusic");
        PlayerPrefs.SetInt("Difficulty2", 0);
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    public void TwoPlayer()
    {
       // AdmobAds.instance.DestroyBannerAd();
        PlayerPrefs.SetInt("Difficulty2", 1);
        Time.timeScale = 1;
        SceneManager.LoadScene(4);
    }
    public void ShopScene()
    {
       // RandomInstertitialAd();
        Time.timeScale = 1;
        SceneManager.LoadScene(5);
    }
    public void GoBack()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void GoBackTwoPlayer()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public static void GameOver() //War vorher static
    {
        // InterstitialAd
        Time.timeScale = 1;
        SceneManager.LoadScene(3);
    }

    public void GoToOptions()
    {
       // RandomInstertitialAd();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    //Ads
    public void ShowBannerAd()
    {
        Time.timeScale = 1;
        //AdmobAds.instance.reqBannerAd();
    }
    public void ShowInterstitialAd()
    {
        Time.timeScale = 1;
        //AdmobAds.instance.ShowInterstitialAd();
    }
    public void RandomInstertitialAd()
    {
        int rnd = Random.Range(0, 2);
        if (rnd == 0)
        {
            //AdmobAds.instance.ShowInterstitialAd();
        }
        Time.timeScale = 1;
    }
}
