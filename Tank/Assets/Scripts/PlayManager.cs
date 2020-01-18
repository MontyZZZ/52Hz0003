using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    public int hp = 3;
    public int score = 0;
    public bool isDead = false;
    public bool isDefeat = false;
    public GameObject born;
    public Text scoreValue;
    public Text hpValue;
    public GameObject overUI;

    private static PlayManager instance;

    public static PlayManager Instance 
    { 
        get => instance; 
        set => instance = value; 
    }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeat)
        {
            overUI.SetActive(true);
            Invoke("returnToLoadScene", 3);
            return;
        }

        if (isDead)
        {
            recover();
        }

        scoreValue.text = score.ToString();
        hpValue.text = hp.ToString();
    }

    private void recover()
    {
        if (hp < 0)
        {
            isDefeat = true;
            Invoke("returnToLoadScene", 3);
        }
        else
        {
            hp--;
            GameObject obj = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            obj.GetComponent<Born>().isPlayer = true;
            isDead = false;
        }   
    }

    private void returnToLoadScene()
    {
        SceneManager.LoadScene("Loading");
    }
}
