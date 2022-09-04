using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class FragenGeneratorTwoPlayers : MonoBehaviour
{
    public int count;
    public GameObject frageButton;
    public GameObject frageText;
    public GameObject inputField;
    public GameObject inputFieldRight;
    string userInput;//Nur intern für UserHS
    public GameObject ergebnisTextUser;
    public GameObject ergenisTextPlayerRight;
    public GameObject confirmButton;
    GameObject confirmButtonRight;

    List<string> fragenListe;
    int listenLength;
    int richtigeAntwort;
    int playerLeftAntwort;
    int playerRightAntwort;

    public GameObject playerRightAntwortText;
    public GameObject richtigeAntwortText;

    public GameObject player;
    public Sprite playerKassiert;
    public Sprite playerNormal;
    public GameObject playerRight;
    public Sprite playerRightKassiert;
    public Sprite playerRightNormal;

    [SerializeField] FragenGenerator fragenGenerator;
    [SerializeField] HealthBar healthBar;

    [SerializeField] HitForce hitForce;
    public GameObject hitForceObj;
    GameObject inputGameobject;
    GameObject playerRightInputGameobject;
    int beginner;
    public bool zweiter = false;
    //private void Awake()
    //{
    //    PlayerPrefs.SetInt("Difficulty", 4);
    //}
    public void Start()
    {
        confirmButton.SetActive(false);
        hitForceObj.SetActive(false);
        inputField.SetActive(false);
        inputGameobject = GameObject.Find("SpielerAntworten");
        playerRightInputGameobject = GameObject.Find("SpielerAntwortenRechts");
        confirmButtonRight = GameObject.Find("BestätigenRechts");
        confirmButtonRight.SetActive(false);
        inputGameobject.SetActive(false);
        playerRightInputGameobject.SetActive(false);
        
    }

    public void RandomFrage()
    {
        if (count > 0)
        {
            //FrageButton verschwinden lassen 
            frageButton.SetActive(false);
            inputField.SetActive(true);
            inputFieldRight.SetActive(true);
            inputGameobject.SetActive(true);
            playerRightInputGameobject.SetActive(true);
            inputField.GetComponent<Text>().text = $"";
            inputFieldRight.GetComponent<Text>().text = $"";

            ergebnisTextUser.GetComponent<Text>().text = $"";
            ergenisTextPlayerRight.GetComponent<Text>().text = $"";
            richtigeAntwortText.GetComponent<Text>().text = $"";

            player.GetComponent<Image>().sprite = playerNormal;
            playerRight.GetComponent<Image>().sprite = playerRightNormal;
            hitForceObj.SetActive(false);

            beginner = PlayerPrefs.GetInt("twoPlayerBeginner");
            zweiter = false;
            if (beginner == 0)
            {
                inputGameobject.SetActive(true);
                playerRightInputGameobject.SetActive(false);
                inputGameobject.GetComponent<InputField>().text = "";
                playerRightInputGameobject.GetComponent<InputField>().text = "";
                confirmButton.SetActive(true);
                confirmButtonRight.SetActive(false);
            }
            else if (beginner == 1)
            {
                playerRightInputGameobject.SetActive(true);
                inputGameobject.SetActive(false);
                inputGameobject.GetComponent<InputField>().text = "";
                playerRightInputGameobject.GetComponent<InputField>().text = "";
                confirmButtonRight.SetActive(true);
                confirmButton.SetActive(false);
            }


            if (count == 99)
            {
                fragenListe = Listefüllen();
                listenLength = fragenListe.Count;
            }
            //Input anzeigen und aufnehmen
            //Input vergleichen und Faust geben

            FrageAnzeigenUndChecken(fragenListe, listenLength);
            listenLength--;
        }
        else
        {
            //Spiel zuende
            throw new System.ArgumentException("Game Over");
        }

    }
    //FragenListe erstellen
    public List<string> Listefüllen()
    {
        //string lines;
        //StreamReader sr = new StreamReader("fragen.txt");
        //List<string> fragenListe = new List<string>();

        ////Liste füllen
        //while (!sr.EndOfStream)
        //{
        //    lines = sr.ReadLine();
        //    fragenListe.Add(lines);
        //}
        //sr.Close();
        //return fragenListe;

        //string readTest = Application.streamingAssetsPath + "\\fragen" + ".txt";
        //List<string> fileLines = File.ReadAllLines(readTest).ToList();
        //return fileLines;
        TextAsset mytxtData = (TextAsset)Resources.Load("fragen");
        List<string> fragenListe = new List<string>();
        var arrayString = mytxtData.text.Split('\n');
        foreach (string line in arrayString)
        {
            fragenListe.Add(line);
        }
        return fragenListe;
    }

    public void FrageAnzeigenUndChecken(List<string> fragenListe, int listenLength)
    {
        //Frage auswählen und aus Liste löschen --> Kommt nicht doppelt vor
        int zugriff = Random.Range(0, listenLength);
        Debug.Log("Zugriff: " + zugriff);

        //Richtige Antwort bekommen
        string[] listenAntwort = fragenListe[zugriff].Split(';');
        foreach (string x in listenAntwort)
        {
            Debug.Log(x);
        }
        int richtigeAntwort = int.Parse(listenAntwort[1]);
        PlayerPrefs.SetInt("richtigeAntwort", richtigeAntwort);

        //Frage im Text anzeigen
        frageText.GetComponent<Text>().text = listenAntwort[0];

        fragenListe.RemoveAt(zugriff);
        count--;
    }

    //UserInput aufnehmen
    public void UserInput()
    {
        //Input des Nutzers Abfrages und Knöpfe an/ausmachen
        int userAntwort = System.Convert.ToInt32(UserHS());
        playerLeftAntwort = userAntwort;
        confirmButton.SetActive(false);
        inputField.SetActive(false);
        //inputFieldRight.SetActive(true);

        //Linker Spieler als erstes
        if (zweiter == false)
        {
            //inputFieldRight.SetActive(true);
            playerRightInputGameobject.SetActive(true);
            confirmButtonRight.SetActive(true);
            confirmButton.SetActive(false);
            //inputField.SetActive(false);
            inputGameobject.SetActive(false);
            zweiter = true;
        }
        else
        {
            inputGameobject.SetActive(false);
            AntwortVergleich2();
        }
        
    }
    public string UserHS()
    {
        userInput = inputField.GetComponent<Text>().text;
        return userInput;
    }
    public void RightUserInput()
    {
        int userAntwort = System.Convert.ToInt32(RightUserHS());
        playerRightAntwort = userAntwort;
        inputFieldRight.SetActive(false);
        confirmButtonRight.SetActive(false);
        inputFieldRight.SetActive(false);

        //rechter Spieler als erstes
        if (zweiter == false)
        {
            //inputField.SetActive(true);
            inputGameobject.SetActive(true);
            confirmButton.SetActive(true);
            confirmButtonRight.SetActive(false);
            playerRightInputGameobject.SetActive(false);
            zweiter = true;
        }
        else
        {
            playerRightInputGameobject.SetActive(false);
            AntwortVergleich2();
        }

    }
    public string RightUserHS()
    {
        return inputFieldRight.GetComponent<Text>().text;
    }


    public void AntwortVergleich2()
    {
        //Richtige Antwort
        int richtigeAntwort = PlayerPrefs.GetInt("richtigeAntwort");
        //Anzeigen der Antworten
        richtigeAntwortText.GetComponent<Text>().text = $"Richtige Antwort: {richtigeAntwort}";
        ergebnisTextUser.GetComponent<Text>().text = $"{playerLeftAntwort}";
        ergenisTextPlayerRight.GetComponent<Text>().text = $"{playerRightAntwort}";
        //Links verliert
        if (System.Math.Abs(playerLeftAntwort - richtigeAntwort) > System.Math.Abs(playerRightAntwort - richtigeAntwort))
        {
            player.GetComponent<Image>().sprite = playerKassiert;
            PlayerPrefs.SetInt("gewinner", 1);
            hitForceObj.SetActive(true);
            
        }
        //Rechts verliert 
        else if (System.Math.Abs(playerLeftAntwort - richtigeAntwort) < System.Math.Abs(playerRightAntwort - richtigeAntwort))
        {
            playerRight.GetComponent<Image>().sprite = playerRightKassiert;
            PlayerPrefs.SetInt("gewinner", 0);
            //Healthbar funktion wird von HitForce aufgerufen um leben abzuziehen ; Erstmal aktivieren
            hitForceObj.SetActive(true);
        }
        else
        {
            Debug.Log("Wie sagt man so schön: 'zwei Dumme ein Gedanke'");
            frageButton.SetActive(true);
        }

    }
}
