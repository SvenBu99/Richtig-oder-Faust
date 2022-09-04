using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderScript : MonoBehaviour
{
    GameObject slider;
    float volume;

    void Start()
    {
        volume = PlayerPrefs.GetFloat("volumeSlider");
    }

    private void OnEnable()
    {
        slider = GameObject.Find("Slider");
        slider.GetComponent<Slider>().onValueChanged.AddListener(ChangeVolume);
    }

    // Update is called once per frame
    private void OnDisable()
    {
        slider.GetComponent<Slider>().onValueChanged.RemoveAllListeners();
    }
    
    public void ChangeVolume(float value)
    {
        volume = slider.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("volumeSlider", volume);
    }
        
}
