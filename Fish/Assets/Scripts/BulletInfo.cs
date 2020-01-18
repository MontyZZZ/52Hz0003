using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo : MonoBehaviour
{
    public float speed;
    public float damage;
    public GameObject web;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Bullet" + damage);
        //damage = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(gameObject);
        }
        else if(collision.tag == "Fish")
        {
            GameObject obj = Instantiate(web);
            obj.transform.SetParent(gameObject.transform.parent, false);
            obj.transform.position = gameObject.transform.position;
            obj.GetComponent<WebInfo>().damage = damage;
            //Debug.Log("Bullet" + damage);
            Destroy(gameObject);

        }
    }
}
