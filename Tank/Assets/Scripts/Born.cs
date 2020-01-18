using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public bool isPlayer;

    // Start is called before the first frame update
    void Start()
    {
        if (isPlayer)
            Invoke("createPlayer", 1f);
        else
            Invoke("createEnemy", 1f);

        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createPlayer()
    {
        Instantiate(player, transform.position, transform.rotation);
    }

    private void createEnemy()
    {
        Instantiate(enemy, transform.position, transform.rotation);
    }
}
