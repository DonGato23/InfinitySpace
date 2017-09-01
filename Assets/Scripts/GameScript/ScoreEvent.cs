using System.Collections;
using System;
using UnityEngine;

public class ScoreEvent : MonoBehaviour {

    public static Action<float> OnSumScorePlayer;

    public static void SumScorePlayer(float score)
    {
        if (OnSumScorePlayer != null)
            OnSumScorePlayer(score);
    }
}
