using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifes : MonoBehaviour
{
    public int maxLifes = 3;
    public int lifes = 5;
    public Sprite Vida;
    public Sprite Blanco;
    public Image Vida1;
    public Image Vida2;
    public Image Vida3;
    public Image Vida4;
    public Image Vida5;
    public GameObject GO;

    private void Start()
    {
        Time.timeScale = 1;
        UpdateLifes();
    }

    private void UpdateLifes()
    {
        switch (maxLifes)
        {
            case 4:
                Vida5.enabled = false;
                break;
            case 3:
                Vida5.enabled = false;
                Vida4.enabled = false;
                break;
            case 2:
                Vida5.enabled = false;
                Vida4.enabled = false;
                Vida3.enabled = false;
                break;
            case 1:
                Vida5.enabled = false;
                Vida4.enabled = false;
                Vida3.enabled = false;
                Vida2.enabled = false;
                break;
            default:
                break;
        }

        switch (lifes)
        {
            case 0:
                Vida1.sprite = Blanco;
                Vida2.sprite = Blanco;
                Vida3.sprite = Blanco;
                Vida4.sprite = Blanco;
                Vida5.sprite = Blanco;
                break;
            case 1:
                Vida1.sprite = Vida;
                Vida2.sprite = Blanco;
                Vida3.sprite = Blanco;
                Vida4.sprite = Blanco;
                Vida5.sprite = Blanco;
                break;
            case 2:
                Vida1.sprite = Vida;
                Vida2.sprite = Vida;
                Vida3.sprite = Blanco;
                Vida4.sprite = Blanco;
                Vida5.sprite = Blanco;
                break;
            case 3:
                Vida1.sprite = Vida;
                Vida2.sprite = Vida;
                Vida3.sprite = Vida;
                Vida4.sprite = Blanco;
                Vida5.sprite = Blanco;
                break;
            case 4:
                Vida1.sprite = Vida;
                Vida2.sprite = Vida;
                Vida3.sprite = Vida;
                Vida4.sprite = Vida;
                Vida5.sprite = Blanco;
                break;
            case 5:
                Vida1.sprite = Vida;
                Vida2.sprite = Vida;
                Vida3.sprite = Vida;
                Vida4.sprite = Vida;
                Vida5.sprite = Vida;
                break;
        }

        if (lifes == 0)
        {
            GameOver();
        }
    }

    public void Bonus()
    {
        if (lifes < maxLifes)
        {
            lifes = lifes + 1;
            UpdateLifes();
        }
    }

    public void Lost()
    {
        if (lifes > 0)
        {
            lifes = lifes - 1;
            UpdateLifes();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        GO.SetActive(true);
    }
}