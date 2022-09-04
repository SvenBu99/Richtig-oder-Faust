using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public GameObject audioSlider;
    public AudioSource audioSource;
    public AudioClip menuAudio;
    public AudioClip fightAudio;    //Zu Array machen
    public AudioClip winAudio;
    public AudioClip loseAudio;
    float volume;
    public static bool playing;
    private static AudioManager audioManagerInstance;

    private void Awake()
    {
        playing = false;
        DontDestroyOnLoad(this);
        if (audioManagerInstance == null)
        {
            audioManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        volume = PlayerPrefs.GetFloat("volumeSlider");
        audioSlider.GetComponent<Slider>().value = volume; //Ohne go
        gameObject.GetComponent<AudioSource>().volume = volume;
    }
    private void Start()
    {
        playing = false;

    }
    private void Update()
    {
        GameObject testSlider = GameObject.Find("Slider");
        //Geht nur wenn man das Menü offen hat
        if (testSlider != null)
        {
            audioSlider = testSlider;
        }
        //Soll kein Error angeben wenn kein Slider da ist
        if (audioSlider == null)
        {
            audioSlider = (GameObject)Resources.Load("Slider");
            audioSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("volumeSlider");
        }
        

        gameObject.GetComponent<AudioSource>().volume = volume;

        if (volume != audioSlider.GetComponent<Slider>().value)
        {
            volume = audioSlider.GetComponent<Slider>().value;
        }
       
        if (SceneManager.GetActiveScene().buildIndex == 3 && (PlayerPrefs.GetInt("gewinnerString") == 1|| PlayerPrefs.GetInt("gewinnerString") == 2|| PlayerPrefs.GetInt("gewinnerString") == 3) && playing == false)
        {
            playing = true;
            this.gameObject.GetComponent<AudioSource>().clip = winAudio;
            audioSource.Play();
        }
        if (SceneManager.GetActiveScene().buildIndex == 3 && PlayerPrefs.GetInt("gewinnerString") == 0 && playing == false)
        {
            playing = true;
            this.gameObject.GetComponent<AudioSource>().clip = loseAudio;
            audioSource.Play();
        }
        if ((SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3) && playing == false)
        {
            playing = true;
            this.gameObject.GetComponent<AudioSource>().clip = fightAudio;
            audioSource.Play();
        }
        if ((SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 5) && playing == false)
        {
            playing = true;
            this.gameObject.GetComponent<AudioSource>().clip = fightAudio;
            audioSource.Play();
        }
        if ((SceneManager.GetActiveScene().buildIndex == 0 /*|| SceneManager.GetActiveScene().buildIndex == 1*/) && playing == false)
        {
            playing = true;
            this.gameObject.GetComponent<AudioSource>().clip = menuAudio;
            audioSource.Play();
        }
    }
   
    //Wichtig für dem Slider 
    public void ChangeVolume()
    {
        volume = audioSlider.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("volumeSlider", volume);
    }

}
