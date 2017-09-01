using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public float Speed;
    public float FireRate;
    public float SumScore;
    public float DamageLife;
    private float _life=100f;
    public GameObject BulletPrefab;
    public GameObject[] ItemsDrop;
    public bool isTutorial;
    private float _timeElapsed;

    private CircleCollider2D _col;
    private GameObject _firstChild;
    private bool _move = true;
    private Animator _animator;
    private Sprite _spriteIndex;

    // Use this for initialization
    void Start()
    {
        _col = GetComponent<CircleCollider2D>();
        _firstChild = transform.GetChild(0).gameObject;
        _animator = GetComponentInChildren<Animator>();
        _spriteIndex = GetComponentInChildren<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        if (_firstChild.activeSelf)
        {
            if (!_firstChild.GetComponent<SpriteRenderer>().isVisible)
            {
                Restart();
            }
        }

        if (_move)
            Shoot();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_move)
            transform.Translate(Vector3.down * Speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Bullet")
        {
            _life -= DamageLife;
            Destroy(col.gameObject);
            if (_life <= 0)
            {
                ScoreEvent.SumScorePlayer(SumScore);
                _animator.SetBool("dead", true);
                _col.enabled = false;
                _move = false;
                DropItem();
                Invoke("Restart", 0.3f);
            }
        }
        else if (col.transform.tag == "Player")
        {
            LifeEvent.SumLifePlayer(-15f);
            _animator.SetBool("dead", true);
            _col.enabled = false;
            _move = false;
            Invoke("Restart", 0.3f);
        }
        else if (col.transform.tag == "Shield")
        {
            _col.enabled = false;
            _animator.SetBool("dead", true);
            _move = false;
            Invoke("Restart", 0.3f);
        }
    }

    void Restart()
    {
        CancelInvoke();
        _animator.SetBool("dead", false);
        _col.enabled = true;
        _move = true;
        GetComponentInChildren<SpriteRenderer>().sprite = _spriteIndex;
        ObjectPool.Instance.PoolAgain(gameObject);
    }

    private void Shoot()
    {
        //Si podemos disparar
        if (_timeElapsed <= 0)
        {
            //Si el usuario apreta el boton Fire1
            //if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
            {
                //Reseteamos el valor que tenemos que esperar
                _timeElapsed = FireRate;
                //Instanciamos una bala en la escena, en la posicion del jugador

                GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
                bullet.transform.parent = gameObject.transform;
                bullet.transform.localPosition = new Vector3(0f, 0f);
                bullet.transform.parent = null;
            }
        }
        else
        {
            //Sino, restamos tiempo al _timeElapsed
            _timeElapsed -= Time.deltaTime;
        }
    }

    void DropItem() {
        if (!isTutorial)
        {
            float rta = Random.Range(0f, 1.5f);
            if (rta < 0.25)
            {
                int rta2 = Random.Range(0, ItemsDrop.Length);
                Instantiate(ItemsDrop[rta2], transform.position, ItemsDrop[rta2].transform.rotation);
            }
        }
    }
}
