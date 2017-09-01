using UnityEngine;
using System.Collections;
using System;

public class LifeEvent : MonoBehaviour {

    public static Action<float> OnSumLifePlayer;

    public static void SumLifePlayer(float daño)
    {
        if (OnSumLifePlayer != null)
            OnSumLifePlayer(daño);
    }

}
