using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{

    public Transform target;
    public float speed = .01f;
    public GameObject Sprite;
    public GameObject Explotion;

    private CircleCollider2D _col;
    private bool _again = false;
    private GameObject _firstChild;

    // Use this for initialization
    void Start()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;
        _col = GetComponent<CircleCollider2D>();
        transform.right = target.position - transform.position;
        _firstChild = transform.GetChild(0).gameObject;
    }

     private void Update()
     {
         if (_firstChild.activeSelf)
         {
             if (!_firstChild.GetComponent<SpriteRenderer>().isVisible)
             {
                 RestartMeteor();
                 _again = true;
             }
         }
     }

    void FixedUpdate()
    {
        if (_again)
        {
            transform.right = target.position - transform.position;
            _again = false;
        }
        //move towards the player
        // if (Vector3.Distance(transform.position, target.position) > 1.5f)
        //{//move if distance from target is greater than 1
        transform.Translate(new Vector3(speed * Time.deltaTime, 0));
        //}
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Bullet")
        {
            Destroy(col.gameObject);
            ScoreEvent.SumScorePlayer(5f);
            Sprite.SetActive(false);
            Explotion.SetActive(true);
            _col.enabled = false;
            Invoke("RestartMeteor", 3f);
        }
        else if (col.transform.tag == "Player")
        {
                LifeEvent.SumLifePlayer(-15f);
                Sprite.SetActive(false);
                Explotion.SetActive(true);
                _col.enabled = false;
                Invoke("RestartMeteor", 3f);
        }
        else if (col.transform.tag == "Shield")
        {
            Sprite.SetActive(false);
            Explotion.SetActive(true);
            _col.enabled = false;
            Invoke("RestartMeteor", 3f);
        }
    }

    void RestartMeteor()
    {
        Sprite.SetActive(true);
        Explotion.SetActive(false);
        _col.enabled = true;
        _again = true;
        ObjectPool.Instance.PoolAgain(gameObject);
    }

}
