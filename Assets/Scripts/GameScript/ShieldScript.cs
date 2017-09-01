using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour {

    public float Duration;
    public GameObject Player;

    private void Start()
    {
        Physics2D.IgnoreCollision(Player.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Bullet")
        {
            Physics2D.IgnoreCollision(col.collider, GetComponent<CircleCollider2D>());
        }

        else if (col.transform.tag == "BulletEnemy")
        {
            Destroy(col.gameObject);
        }
    }

    public void ActivateShield() {
        CancelInvoke();
        gameObject.SetActive(true);
        PlayerControl.shield = true;
        Invoke("DeactivateShield", Duration);
    }

    public void DeactivateShield()
    {
        PlayerControl.shield = false;
        gameObject.SetActive(false);
    }

}
