using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour {

    public float _currentLife=100f;
    public DamageScript Damage;
    public PlayerControl Player;

    public void Life(float vida)
    {
        if (vida > 0)
        {
            _currentLife += vida - _currentLife;
        }
        else { _currentLife += vida; }

        if (_currentLife <= 0)
        {
            Player.Dead();
        }
        else if (_currentLife < 25)
        {
            Damage.ChangeSprite(3);
        }
        else if (_currentLife < 50)
        {
            Damage.ChangeSprite(2);
        }
        else if (_currentLife < 80)
        {
            Damage.ChangeSprite(1);
        }
        else if (_currentLife > 80) {
            Damage.ChangeSprite(0);
        }
    }

    void OnEnable()
    {
        LifeEvent.OnSumLifePlayer += SumLife;
    }

    void OnDisable()
    {
        LifeEvent.OnSumLifePlayer -= SumLife;
    }

    void SumLife(float daño)
    {
        Life(daño);
    }
}
