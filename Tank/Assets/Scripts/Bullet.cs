using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public bool isFromPlay;

    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                if (!isFromPlay)
                    collision.SendMessage("tankDie");
                break;
            case "Enemy":
                if (isFromPlay)
                    collision.SendMessage("enemyDie");
                break;
            case "Wall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "Base":
                collision.SendMessage("gameOver");
                Destroy(gameObject);
                break;
            case "IronWall":
                if (isFromPlay)
                {
                    collision.SendMessage("playAudio");
                }
                Destroy(gameObject);
                break;
            case "AirWall":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
