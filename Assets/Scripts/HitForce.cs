using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitForce : MonoBehaviour
{
    Slider slider;
    public GameObject slideHandleArea;
    public GameObject slideHandler;
    float slideMovement;

    public GameObject schadensDisplay;
    public GameObject stoppButton;
    //public GameObject generiereFrageBtn;
    [SerializeField] HealthBar healthBar;
    void Awake()
    {
        slider = gameObject.GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value == 1)
        {
            slideMovement = -0.6f;
        }
        else if (slider.value == 0)
        {
            slideMovement = 0.6f;
        }
        slider.value += (Time.deltaTime / slideMovement);

    }
    public void StopFunction()
    {
        float sliderPosition = slider.value;
        //generiereFrageBtn.SetActive(true);
        healthBar.Leben(sliderPosition);
    }

    public void StopFunctionTwoPlayer()
    {
        float sliderPosition = slider.value;
        healthBar.LebenTwoPlayer(sliderPosition);
    }
    
 
}
