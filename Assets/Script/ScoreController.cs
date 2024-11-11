using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ScoreController : NetworkBehaviour
{
    public Text redScoreText;
    public Text blueScoreText;

    [SyncVar]
    public int redScore;
    [SyncVar]
    public int blueScore;

    public static ScoreController singleton;
    void Start()
    {
        singleton = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddScore(int r, int b)
    {
        redScore += r;
        blueScore += b;

        redScoreText.text = redScore.ToString();
        blueScoreText.text = blueScore.ToString();
    }
}
