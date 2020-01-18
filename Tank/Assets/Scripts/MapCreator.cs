using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCreator : MonoBehaviour
{
    // all items
    public GameObject[] items; // base  wall ironwall airwall water grass born


    private List<Vector3> itemPosList = new List<Vector3>();

    private void Awake()
    {
        createBase();
        createAirWall();
        createTerrain();
        createPlayer();

        InvokeRepeating("createEnemy", 4, 5);

    }

    private void createItem(GameObject gobject, Vector3 position, Quaternion rotation) 
    {
        GameObject obj = Instantiate(gobject, position, rotation);
        obj.transform.SetParent(gameObject.transform);
        itemPosList.Add(position);
    }

    private void createBase()
    {
        createItem(items[0], new Vector3(0, -8, 0), Quaternion.identity);
        createItem(items[1], new Vector3(-1, -8, 0), Quaternion.identity);
        createItem(items[1], new Vector3(1, -8, 0), Quaternion.identity);

        for (int i = -1; i < 2; i++)
        {
            createItem(items[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
    }

    private void createAirWall()
    {
        for (int i = -11; i < 12; i++)
        {
            createItem(items[3], new Vector3(i, 9, 0), Quaternion.identity);
            createItem(items[3], new Vector3(i, -9, 0), Quaternion.identity);
        }

        for (int i = -8; i < 9; i++)
        {
            createItem(items[3], new Vector3(-11, i, 0), Quaternion.identity);
            createItem(items[3], new Vector3(11, i, 0), Quaternion.identity);
        }
    }
    private Vector3 createRadomPos()
    {
        while (true)
        {
            Vector3 pos = new Vector3(Random.Range(-9, 10), 
                Random.Range(-7, 8), 0);

            if (!itemPosList.Contains(pos))
                return pos;
        }
    }


    private void createTerrain()
    {
        // wall
        for (int i = 0; i < 60; i++)
        {
            Vector3 pos = createRadomPos();
            createItem(items[1], pos, Quaternion.identity);
        }

        // ironWall
        for (int i = 0; i < 20; i++)
        {
            Vector3 pos = createRadomPos();
            createItem(items[2], pos, Quaternion.identity);
        }

        // water
        for (int i = 0; i < 20; i++)
        {
            Vector3 pos = createRadomPos();
            createItem(items[4], pos, Quaternion.identity);
        }


        // grass
        for (int i = 0; i < 20; i++)
        {
            Vector3 pos = createRadomPos();
            createItem(items[5], pos, Quaternion.identity);
        }
    }

    private void createPlayer()
    {
        GameObject play1 = Instantiate(items[6], new Vector3(-2, -8, 0), Quaternion.identity);
        play1.GetComponent<Born>().isPlayer = true;

        // three enemy
        Instantiate(items[6], new Vector3(-10, 8, 0), Quaternion.identity);
        Instantiate(items[6], new Vector3(0, 8, 0), Quaternion.identity);
        Instantiate(items[6], new Vector3(10, 8, 0), Quaternion.identity);
    }

    private void createEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 pos;
        if (num == 0)
        {
            pos = new Vector3(-10, 8, 0);
        }
        else if (num == 1)
        {
            pos = new Vector3(0, 8, 0);
        }
        else
        {
            pos = new Vector3(10, 8, 0);
        }
        Instantiate(items[6], pos, Quaternion.identity);
    }
}
