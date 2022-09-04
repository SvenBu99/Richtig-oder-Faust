using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TwoPlayerSelection : MonoBehaviour
{
    public GameObject eigendlichesGame;
    public GameObject auswahlWheel;
    public GameObject auswahlSlider;
    public GameObject auswahlText;

    Slider slidervalue;
    float sliderPosition;
    float randomDur;
    float sliderMovement;

    // Start is called before the first frame update
    void Start()
    {
        eigendlichesGame.SetActive(false);
        auswahlWheel.SetActive(true);
        auswahlText.SetActive(false);
        slidervalue = auswahlSlider.GetComponent<Slider>();
        StartCoroutine(SliderAuswahlBewegen());
    }

    // Update is called once per frame
    void Update()
    {

        if (slidervalue.value == 1)
        {
            sliderMovement = -0.3f;
        }
        else if (slidervalue.value == 0)
        {
            sliderMovement = 0.3f;
        }
        slidervalue.value += (Time.deltaTime / sliderMovement);

    }

    IEnumerator SliderAuswahlBewegen()
    {
        float rnd = Random.Range(1.5f, 2f);     //1.5 bis 4.5

        yield return new WaitForSeconds(rnd);
        sliderPosition = slidervalue.value;
        enabled = false;                    //Update ausstellen
        if (sliderPosition < 0.5)
        {
            PlayerPrefs.SetInt("twoPlayerBeginner", 0); //Links beginnt
        }
        if (sliderPosition >= 0.5)
        {
            PlayerPrefs.SetInt("twoPlayerBeginner", 1);//Rechts beginnt
        }
        auswahlText.SetActive(true);
        if (PlayerPrefs.GetInt("twoPlayerBeginner") == 0)
        {
            auswahlText.GetComponent<TMP_Text>().text = "Spieler links beginnt";
        }
        else if (PlayerPrefs.GetInt("twoPlayerBeginner") == 1)
        {
            auswahlText.GetComponent <TMP_Text>().text = "Spieler rechts beginnt";
        }
        yield return new WaitForSeconds(1f);    //Noch auf 3
        eigendlichesGame.SetActive(true);
        auswahlWheel.SetActive(false);
    }

}
