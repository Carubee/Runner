using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public Slider time; 
    public float maxTime;
    public GameObject fill;

    public GameObject panelText;
    public Text wonText;

    public int scoreToWin;
    public int scoreWin;
    void Start()
    {
        time.maxValue = maxTime;
        time.value = time.maxValue;
    }

    void Update()
    {
        FoundWinner();

        if (time.value <= time.minValue)
        {
            fill.SetActive(false);
            panelText.SetActive(true);
        }
        else
        {
            //time.value -= Time.deltaTime;
        }
    }
    void FoundWinner()
    {
         scoreWin = ScoreController.singleton.redScore - ScoreController.singleton.blueScore;
        if (scoreWin <= -10)
        {
            panelText.SetActive(true);

            wonText.text = "BLUE WIN";
        }
        else if(scoreWin >= 10)
        {
            panelText.SetActive(true);

            wonText.text = "RED WIN";
        }
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(0);

    }
}
