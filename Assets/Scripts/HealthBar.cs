using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    MainMenu mainMenu;
    public float playerLeben;
    public float computerLeben;
    int currentDifficulty;
    public GameObject playerLebenLabel;
    public GameObject playerLebenImage;
    public GameObject computerLebenLabel;
    public GameObject computerLebenImage;
    //Wichtig für Visualisierung
    float playerBarSize;
    float computerBarSize;
    [SerializeField] HitForce hitForce;
    public GameObject hitForceObj;
    public GameObject generiereFrageBtn;

    public AudioSource audioSource;
    float volume;
    //public AudioClip fightMusic;
    AudioClip midPunchSound;
    AudioClip weakPunchSound;
    AudioClip strongPunchSound;
    AudioClip painSound;

    private void Awake()
    {
        currentDifficulty = PlayerPrefs.GetInt("Difficulty");
        volume = PlayerPrefs.GetFloat("volumeSlider");
        audioSource.volume = volume*1.2f;
        //audioSource.volume = volume;
        Debug.Log("currentdifficulty: " + currentDifficulty);
        if (PlayerPrefs.GetInt("Difficulty2") == 1)
        {
            computerLeben = 100;
        }
        else
        {
            if (currentDifficulty == 1)
            {
                computerLeben = 80;
            }
            else if (currentDifficulty == 2)
            {
                computerLeben = 100;
            }
            else if (currentDifficulty == 3)
            {
                computerLeben = 135;
            }
        }
    }

    //EVTL noch Leben in Nummern hinschreiben
    public void Start()
    {
        playerLebenImage.gameObject.GetComponent<Image>().color = Color.green;
        computerLebenImage.gameObject.GetComponent<Image>().color = Color.green;
        /*Musik spielen*/
        midPunchSound = (AudioClip)Resources.Load("seriousPunch");
        weakPunchSound = (AudioClip)Resources.Load("weakPunch");
        strongPunchSound = (AudioClip)Resources.Load("opPunch");
        painSound = (AudioClip)Resources.Load("painSound");

    }
    public void Leben(float nadelpos)
    {
        int damage = SchadensAmount(nadelpos);
        Debug.Log(damage);
        computerLeben -= damage;
        computerBarSize = computerLeben / 100;
        //Sound spielen
        audioSource.Play();
        //Farben ändern
        if (computerBarSize <= 0.6)
        {
            computerLebenImage.gameObject.GetComponent<Image>().color = Color.yellow;
            if (computerBarSize <= 0.4)
            {
                computerLebenImage.gameObject.GetComponent<Image>().color = Color.red;
            }
        }
        computerLebenLabel.transform.localScale = new Vector3(computerBarSize, 1f);
        hitForceObj.SetActive(false);
        //Fragebutton active machen
        generiereFrageBtn.SetActive(true);

        if (computerLeben <= 0)
        {
            PlayerPrefs.SetInt("gewinnerString", 1);
            StartCoroutine(PlaySound());
        }
    }

    public void LebenTwoPlayer(float nadelpos)
    {
        int damage = SchadensAmount(nadelpos);
        Debug.Log(damage);
        //Links gewonnen
        if (PlayerPrefs.GetInt("gewinner") == 0)
        {
            computerLeben -= damage;
            computerBarSize = computerLeben / 100;
            //Sound spielen
            audioSource.Play();
            //Farben ändern
            if (computerBarSize <= 0.6)
            {
                computerLebenImage.gameObject.GetComponent<Image>().color = Color.yellow;
                if (computerBarSize <= 0.4)
                {
                    computerLebenImage.gameObject.GetComponent<Image>().color = Color.red;
                }
            }
            computerLebenLabel.transform.localScale = new Vector3(computerBarSize, 1f);
        }
        //Rechts gewonnen
        if (PlayerPrefs.GetInt("gewinner") == 1)
        {
            playerLeben -= damage;
            playerBarSize = playerLeben / 100;

            audioSource.Play();
            //Farben ändern
            if (playerBarSize <= 0.6)
            {
                playerLebenImage.gameObject.GetComponent<Image>().color = Color.yellow;
                if (playerBarSize <= 0.4)
                {
                    playerLebenImage.gameObject.GetComponent<Image>().color = Color.red;
                }
            }
            playerLebenLabel.transform.localScale = new Vector3(playerBarSize, 1f);
        }

        hitForceObj.SetActive(false);
        generiereFrageBtn.SetActive(true);

        //Wenn einer verloren hat
        if (computerLeben <= 0)
        {
            PlayerPrefs.SetInt("gewinnerString", 2);
            StartCoroutine(PlaySound());
        }
        if (playerLeben <= 0)
        {
            PlayerPrefs.SetInt("gewinnerString", 3);
            StartCoroutine(PlaySound());
        }
    }
    public void LebenComputerWin()
    {
        int damage = RandomSchadenPlayer();
        playerLeben -= damage;
        playerBarSize = playerLeben / 100;

        //Farben ändern
        if (playerBarSize <= 0.6)
        {
            playerLebenImage.gameObject.GetComponent<Image>().color = Color.yellow;
            if (playerBarSize <= 0.4)
            {
                playerLebenImage.gameObject.GetComponent<Image>().color = Color.red;
            }
        }
        playerLebenLabel.transform.localScale = new Vector3(playerBarSize, 1f);

        //Fragebutton
        generiereFrageBtn.SetActive(true);
        //Check if Spiel vorbei
        if (playerLeben <= 0)
        {
            PlayerPrefs.SetInt("gewinnerString", 0);
            StartCoroutine(PlaySound());
        }
    }
    public int RandomSchadenPlayer()
    {
        int dmg = 0;
        if (currentDifficulty == 1)
        {
            dmg = Random.Range(15, 20);//Hier noch float machen
        }
        else if (currentDifficulty == 2)
        {
            dmg = Random.Range(20, 28);//Hier noch float machen
        }
        else if (currentDifficulty == 3)
        {
            dmg = Random.Range(20, 35);//Hier noch float machen
        }
        if (dmg < 20)
        {
            audioSource.clip = weakPunchSound;
            audioSource.Play();
        }
        else if (dmg < 25 && dmg > 20)
        {
            audioSource.clip = midPunchSound;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = strongPunchSound;
            audioSource.Play();
        }
        Debug.Log(dmg);
        return dmg;
    }
    public int SchadensAmount(float sliderValue)
    {
        Debug.Log("Sliderposition: " + sliderValue);
        float distance = 0.5f - sliderValue;
        int dmgOutput;
        if (System.Math.Abs(distance) <= 0.05)
        {
            dmgOutput = 30;
            audioSource.clip = strongPunchSound;
        }
        else if (System.Math.Abs(distance) <= 0.15)
        {
            dmgOutput = 20;
            audioSource.clip = midPunchSound;
        }
        else
        {
            dmgOutput = 15;
            audioSource.clip = weakPunchSound;
        }
        return dmgOutput;
    }

    IEnumerator PlaySound()
    {
        audioSource.clip = painSound;
        audioSource.Play();
        yield return new WaitUntil(() => audioSource.isPlaying == false); //Warten bis Sound abgespielt ist
        MainMenu.GameOver();
    }
}
