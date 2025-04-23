using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GemManager : MonoBehaviour
{
    public TMP_Text text; // Texto que muestra el progreso
    int totalGems;


    public TMP_Text levelCleared; // Texto que muestra "Has superado el juego"

    void Start()
    {
        totalGems = transform.childCount;
        UpdateGemText();
        levelCleared.gameObject.SetActive(false); // Oculta el texto al inicio
    }

    void Update()
    {
        UpdateGemText();
    }

    void UpdateGemText()
    {
        int currentGems = transform.childCount;
        text.text = "Gemas recolectadas: " + (totalGems - currentGems) + " / " + totalGems;

        if (currentGems == 0)
        {
            levelCleared.gameObject.SetActive(true); // Muestra el mensaje de victoria
        }
    }
}
