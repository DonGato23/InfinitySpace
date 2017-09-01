using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControlTutorial : MonoBehaviour {

    
    public float Speed;
    public GameObject BulletPrefab;
    public float FireRate = 0.3f;
    float _timeElapsed;


    // Update is called once per frame
    void FixedUpdate () {
        if (TutorialGameManagerScript.ActionNumber == 0)
        {
            Move();
        }
        else if (TutorialGameManagerScript.ActionNumber > 0)
        {
            Move();
            Shoot();
        }
    }

    private void Move()
    {
        if (Input.GetMouseButton(0))
        {
            var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);
        }
    }

    public void Shoot()
    {
        //Si podemos disparar
        if (_timeElapsed <= 0)
        {
            //Si el usuario apreta el boton Fire1
            //if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
            //Reseteamos el valor que tenemos que esperar
            _timeElapsed = FireRate;
            //Instanciamos una bala en la escena, en la posicion del jugador

            GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
            bullet.transform.parent = gameObject.transform;
            bullet.transform.localPosition = new Vector3(0f, 0.4149f);
            bullet.transform.parent = null;
        }
        else
        {
            //Sino, restamos tiempo al _timeElapsed
            _timeElapsed -= Time.deltaTime;
        }
    }
}
