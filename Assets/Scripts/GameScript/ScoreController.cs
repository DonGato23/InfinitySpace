using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

   
   // public Text ScoreText;

    public void Score(float score)
    {
        PlayerControl._currentScore += score;
        //ScoreText.text = PlayerControl._currentScore.ToString();
    }

    void OnEnable()
    {
        ScoreEvent.OnSumScorePlayer += SumScore;
    }

    void OnDisable()
    {
        ScoreEvent.OnSumScorePlayer -= SumScore;
    }

    void SumScore(float score)
    {
        Score(score);
    }
}
