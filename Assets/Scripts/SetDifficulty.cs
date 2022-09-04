using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDifficulty : MonoBehaviour
{
    int currentDifficulty;
    GameObject easyPic;
    GameObject midPic;
    GameObject hardPic;
    //public AudioSource audioSource;
    AudioClip clickSound;

    private void Awake()
    {
        easyPic = GameObject.Find("ImageCat");
        midPic = GameObject.Find("ImageWolf");
        hardPic = GameObject.Find("ImageLion");
    }
    void Update()
    {
        currentDifficulty = PlayerPrefs.GetInt("Difficulty");
        if (currentDifficulty == 1)
        {
            easyPic.GetComponent<Image>().color = Color.green;
            midPic.GetComponent<Image>().color = Color.white;
            hardPic.GetComponent<Image>().color = Color.white;
        }
        else if (currentDifficulty == 2)
        {
            easyPic.GetComponent<Image>().color = Color.white;
            midPic.GetComponent<Image>().color = Color.green;
            hardPic.GetComponent<Image>().color = Color.white;
            //audioSource.clip = clickSound;
        }
        else if (currentDifficulty == 3)
        {
            easyPic.GetComponent<Image>().color = Color.white;
            midPic.GetComponent<Image>().color = Color.white;
            hardPic.GetComponent<Image>().color = Color.green;
            //audioSource.clip = clickSound;
        }
    }

    public void SetEasy()
    {
        PlayerPrefs.SetInt("Difficulty", 1);
    }
    public void SetMid()
    {
        PlayerPrefs.SetInt("Difficulty", 2);
    }
    public void SetHard()
    {
        PlayerPrefs.SetInt("Difficulty", 3);
    }
}
