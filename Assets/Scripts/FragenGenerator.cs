using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class FragenGenerator : MonoBehaviour
{
    public int count;
    public GameObject frageButton;
    public GameObject frageText;
    public GameObject inputField;
    string userInput;
    public GameObject ergebnisTextUser;
    public GameObject confirmButton;

    List<string> fragenListe;
    int listenLength;
    int richtigeAntwort;

    public GameObject computerAntwortText;
    public GameObject richtigeAntwortText;

    public GameObject player;
    public Sprite playerKassiert;
    public Sprite playerNormal;
    public GameObject computer;
    public Sprite computerKassiert;
    public Sprite computerNormal;

    [SerializeField] HealthBar healthBar;

    [SerializeField] HitForce hitForce;
    public GameObject hitForceObj;
    GameObject inputGameobject;

    public void Start()
    {
        confirmButton.SetActive(false);
        hitForceObj.SetActive(false);
        inputField.SetActive(false);
        inputGameobject = GameObject.Find("SpielerAntworten");
        inputGameobject.SetActive(false);
    }

    public void RandomFrage()
    {
        if (count > 0)
        {
            //FrageButton verschwinden lassen 
            frageButton.SetActive(false);
            inputField.SetActive(true);
            confirmButton.SetActive(true);
            ergebnisTextUser.GetComponent<Text>().text = $"";
            richtigeAntwortText.GetComponent<Text>().text = $"";
            computerAntwortText.GetComponent<Text>().text = $"";
            player.GetComponent<Image>().sprite = playerNormal;
            computer.GetComponent<Image>().sprite = computerNormal;
            hitForceObj.SetActive(false);
            //Inputfeld wieder auftauchen lassen
            inputGameobject.SetActive(true);
            inputGameobject.GetComponent<InputField>().text = "";
            
            
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

        //Liste für die Android App
        TextAsset mytxtData = (TextAsset)Resources.Load("fragen");
        //string readTest = Application.streamingAssetsPath + "\\fragen" + ".txt";
        //List<string> fileLines = File.ReadAllLines(readTest).ToList();

        //StreamReader sr = new StreamReader("Resources/fragen.txt");
        List<string> fragenListe = new List<string>();
        var arrayString = mytxtData.text.Split('\n');

        foreach(string line in arrayString)
        {
            fragenListe.Add(line);
        }

        ////Liste füllen
        //while (!sr.EndOfStream)
        //{
        //    lines = sr.ReadLine();
        //    fragenListe.Add(lines);
        //}
        //sr.Close();
     
        return fragenListe;
       // return fileLines;
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
        frageButton.SetActive(true);
        confirmButton.SetActive(false);

        //ComputerAntwort errechnen und anzeigen
        richtigeAntwort = PlayerPrefs.GetInt("richtigeAntwort");
        int cpAntwort= ComputerAntwort(richtigeAntwort);
        Debug.Log("Computerantwort: " + cpAntwort);

        //Generiere Frage Button ausschalten
        GameObject generiereFrage = GameObject.Find("GeneriereFrageBtn");
        generiereFrage.SetActive(false);
        inputGameobject.SetActive(false);

        //Richtige Antwort anzeigen und Leben abziehen
        richtigeAntwortText.GetComponent<Text>().text = $"Richtige Antwort: {richtigeAntwort}";
        AntwortVergleichen(cpAntwort, richtigeAntwort, userAntwort);
    }
    public string UserHS()
    {
        userInput = inputField.GetComponent<Text>().text;        
        ergebnisTextUser.GetComponent<Text>().text = $"{userInput}";
        //Debug.Log(userInput);
        return userInput;
    }

    //ComputerAntwort generieren und Ausgeben
    public int ComputerAntwort(int richtigeAntwort)
    {
        int computerAntwort = Random.Range(System.Convert.ToInt32(0.33 * richtigeAntwort), System.Convert.ToInt32(1.5 * richtigeAntwort));
        computerAntwortText.GetComponent<Text>().text = $"{computerAntwort}";
        return computerAntwort;
    }

    public void AntwortVergleichen(int computerAntwort, int richtigeAntwort, int userAntwort)
    {
        //User verliert
        if (System.Math.Abs(computerAntwort - richtigeAntwort) < System.Math.Abs(userAntwort - richtigeAntwort))
        {
            player.GetComponent<Image>().sprite = playerKassiert;
            healthBar.LebenComputerWin();
        }
        //User gewinnt
        else if (System.Math.Abs(computerAntwort - richtigeAntwort) >System.Math.Abs( userAntwort - richtigeAntwort))
        {
            computer.GetComponent<Image>().sprite = computerKassiert;
            //Healthbar funktion wird von HitForce aufgerufen um leben abzuziehen ; Erstmal aktivieren
            hitForceObj.SetActive(true);
        }
        else
        {
            Debug.Log("Wie sagt man so schön: 'zwei Dumme ein Gedanke'");
            inputGameobject.SetActive(false);
            frageButton.SetActive(true);
        }
    }

}
