using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public static int Lives=3;
    public static float _currentScore = 0f;
    public static bool shield=false;

    public float Speed=3;
    public GameObject BulletPrefab;
    public float FireRate = 0.3f;
    public ShieldScript Shield;

    private int _powerValue = 0;

    float _timeElapsed;
    Animator _animator;

    // Use this for initialization
    void Start () {
        if (StageManager.GameOver)
            gameObject.SetActive(false);
        _animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //MoveMouse();
        if (StageManager.PlayGame)
        {
            Shoot();
            MoveKeyboard();
            MoveMouse();
        }
    }


    void MoveMouse()
    {
        //transform.position = Vector3.MoveTowards(transform.position, Input.mousePosition, Speed * Time.deltaTime);
        if (Input.GetMouseButton(0))
        {
            var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);
        }
    }

    void MoveKeyboard()
    {
        //Tomamos el input horizontal y lo guardamos en la variables h
        float h = Input.GetAxis("Horizontal");
        //Tomamos el input vertical y lo guardamos en la variables v
        float v = Input.GetAxis("Vertical");
        Vector3 _nextPositionPlayer = transform.position + new Vector3(h, v, 0) * Speed * Time.deltaTime;

        if (InBoundariesX(_nextPositionPlayer, Camera.main) == true)
        {
            transform.position = transform.position + new Vector3(h, 0, 0) * Speed * Time.deltaTime;
        }

        if (InBoundariesY(_nextPositionPlayer, Camera.main) == true)
        {
            transform.position = transform.position + new Vector3(0, v, 0) * Speed * Time.deltaTime;
        }

    }

    public static bool InBoundariesX(Vector3 pos, Camera cam)
    {
        Vector3 posInScreen = cam.WorldToScreenPoint(pos);

        if (posInScreen.x > 0 && posInScreen.x < cam.pixelRect.width)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public static bool InBoundariesY(Vector3 pos, Camera cam)
    {
        Vector3 posInScreen = cam.WorldToScreenPoint(pos);

        if (posInScreen.y > 0 && posInScreen.y < cam.pixelRect.height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Shoot()
    {
        //Si podemos disparar
        if (_timeElapsed <= 0)
        {
            //Si el usuario apreta el boton Fire1
            //if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
            if (_powerValue == 0)
            {
                //Reseteamos el valor que tenemos que esperar
                _timeElapsed = FireRate;
                //Instanciamos una bala en la escena, en la posicion del jugador

                GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
                bullet.transform.parent = gameObject.transform;
                bullet.transform.localPosition = new Vector3(0f, 0.4149f);
                bullet.transform.parent = null;
            }
            else if (_powerValue == 1)
            {
                _timeElapsed = FireRate;

                GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
                bullet.transform.parent = gameObject.transform;
                bullet.transform.localPosition = new Vector3(0.43f, 0.34f);
                bullet.transform.parent = null;

                GameObject bullet2 = Instantiate(BulletPrefab, transform.position, transform.rotation);
                bullet2.transform.parent = gameObject.transform;
                bullet2.transform.localPosition = new Vector3(-0.43f, 0.34f);
                bullet2.transform.parent = null;
            }
        }
        else
        {
            //Sino, restamos tiempo al _timeElapsed
            _timeElapsed -= Time.deltaTime;
        }
    }

    public void Dead() {
        _animator.enabled = true;
        GetComponent<CircleCollider2D>().enabled = false;
        transform.GetChild(2).gameObject.SetActive(false); // damage
        this.enabled = false;
        if(!StageManager.GameOver)
            Invoke("RestLive", 1.5f);
    }

    void RestLive()
    {
        StageManager.RestLive();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "BulletEnemy")
        {
            if (!shield)
            {
                LifeEvent.SumLifePlayer(-15f);
                col.gameObject.SetActive(false);
            }
        }
        else if (col.transform.tag == "ShieldItem")
        {
            ScoreEvent.SumScorePlayer(10f);
            Destroy(col.gameObject);
            Shield.ActivateShield();
        }
        else if (col.transform.tag == "LifeItem") {

            Destroy(col.gameObject);
            LifeEvent.SumLifePlayer(100f);
            ScoreEvent.SumScorePlayer(10f);
        }
        else if (col.transform.tag == "PowerUpItem")
        {
            ScoreEvent.SumScorePlayer(10f);
            Destroy(col.gameObject);
            if(_powerValue<1)
                _powerValue++;
        }
        else if (col.transform.tag == "SpeedShootItem")
        {
            ScoreEvent.SumScorePlayer(10f);
            Destroy(col.gameObject);
            if (FireRate >0.1)
                FireRate-=0.05f;
        }
    }
}
