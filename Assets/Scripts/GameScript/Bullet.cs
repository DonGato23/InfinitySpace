using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float Speed = 30;
    public float Lifetime = 2;
    public bool IsEnemyBullet=false;

    // Use this for initialization
    void Start()
    {
        //Llamar al metodo Autodestroy despues de "Lifetime" segundos
        Invoke("Autodestroy", Lifetime);
        //Otra alternativa es, llamar al destroy despues de "Lifetime" segundos
        //Destroy(gameObject, Lifetime);
    }

    /// <summary>
    /// Destroys the gameObject
    /// </summary>
    void Autodestroy()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsEnemyBullet)
		    transform.position += transform.up * Speed * Time.deltaTime;
        else
            transform.position += (-transform.up) * Speed * Time.deltaTime;
    }

}
