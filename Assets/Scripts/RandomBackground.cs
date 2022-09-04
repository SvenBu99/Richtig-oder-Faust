using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBackground : MonoBehaviour
{
    public Sprite[] backgroundSprites;
    public Image backgroundGameobject;
    public void Awake()
    {
        int auswahl = Random.Range(0, backgroundSprites.Length);
        backgroundGameobject.GetComponent<Image>().sprite = backgroundSprites[auswahl];
    }
}
