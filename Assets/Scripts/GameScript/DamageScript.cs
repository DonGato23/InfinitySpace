using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour {

    public Sprite[] DamageSprites;
    private SpriteRenderer _render;

    private void Awake()
    {
        _render = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(int indice) {
        if (indice < DamageSprites.Length)
            _render.sprite = DamageSprites[indice];
        else
            Debug.Log("Te pasaste en el indice macho.");
    }
}
