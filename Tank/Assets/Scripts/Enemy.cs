using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject boom;
    public Sprite[] enemySprits; // up down right left
    public GameObject bullet;
    public float speed;

    private SpriteRenderer sr;
    private Vector3 bulletAng;
    private float timeBullet;
    private float turnTime;
    private float h;
    private float v = -1;

    void Start()
    {
        speed = 3;
        sr = GetComponent<SpriteRenderer>();
        bulletAng = new Vector3(0, 0, 0);
    }


    void Update()
    {
        if (timeBullet > 3f)
        {
            enemyAttack();
        }
        else
        {
            timeBullet += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        enemyMove();
    }


    private void enemyMove()
    {
        if (turnTime > 4f)
        {
            int num = Random.Range(0, 10);
            if (num > 5)
            {
                // down
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                // up
                v = 1;
                h = 0;
            }
            else if (num >0 && num <= 2)
            {
                // left
                v = 0;
                h = -1;
            }
            else if (num > 2 && num <= 4)
            {
                // right 
                v = 0;
                h = 1;
            }
            turnTime = 0;
        }
        else
        {
            turnTime += Time.fixedDeltaTime;
        }

        transform.Translate(Vector3.right * h * Time.fixedDeltaTime * speed, Space.World);
        if (h > 0)
        {
            // right
            sr.sprite = enemySprits[2];
            bulletAng.z = -90;
        }
        else if (h < 0)
        {
            // left
            sr.sprite = enemySprits[3];
            bulletAng.z = 90;
        }

        if (h != 0)
        {
            return;
        }

        // 垂直
        //Debug.Log("h: " + v);
        transform.Translate(Vector3.up * v * Time.fixedDeltaTime * speed, Space.World);
        if (v > 0)
        {
            // up
            sr.sprite = enemySprits[0];
            bulletAng.z = 0;
        }
        else if (v < 0)
        {
            // down
            sr.sprite = enemySprits[1];
            bulletAng.z = 180;
        }

    }

    private void enemyAttack()
    {
        Instantiate(bullet, transform.position, Quaternion.Euler(transform.eulerAngles + bulletAng));
        bullet.GetComponent<Bullet>().isFromPlay = false;
        timeBullet = 0;
    }

    private void enemyDie()
    {
        PlayManager.Instance.score++;
        Instantiate(boom, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            turnTime = 4;
        }
    }
}
