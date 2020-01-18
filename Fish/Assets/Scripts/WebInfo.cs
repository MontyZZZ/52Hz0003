using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebInfo : MonoBehaviour
{
    public float disappearTime;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, disappearTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fish")
        {
            collision.SendMessage("getDamage", damage);
        }
    }

}
