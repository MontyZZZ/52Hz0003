using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    private int choice = 1;
    public Transform posOne;
    public Transform posTwo;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("WWWWW");
            choice = 1;
            transform.position = posOne.position;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("SSSSSS");
            choice = 2;
            transform.position = posTwo.position;
        }

        if (choice == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game");
        }

    }
}
