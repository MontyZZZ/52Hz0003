using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameControl : MonoBehaviour
{
    private static GameControl instance;
    //public static GameControl Instance { get => instance; set => instance = value; }
    public static GameControl Instance
    {
        get
        {
            return instance;
        }
    }


    public Text costText;
    public Text goldText;
    public Text lvText;
    public Text lvNameText;
    public Text smallCountText;
    public Text bigCountText;
    public Button bigCountButton;
    public Button backButton;
    public Button settingButton;
    public Slider expSlider;

    public int lv = 0;
    public int exp = 0;
    public int gold = 500;
    public const int bigCount = 240;
    public const int smallCount = 60;
    public float bigTimer = bigCount;
    public float smallTimer = smallCount;
    public Color goldColor;
    public int bgIndex;

    public Image bgImage;
    public GameObject lvTips;
    public GameObject seaWaveEffect;
    public GameObject fireEffect;
    public GameObject changEffect;
    public GameObject lvEffect;
    public GameObject goldEffect;


    public Transform bulletHolder;
    public Sprite[] bgSprites;
    public GameObject[] guns;
    public GameObject[] bullet2;
    public GameObject[] bullet3;
    public GameObject[] bullet4;
    public GameObject[] bulletS;
    public GameObject[] bullet1000;

    private int costIndex = 0;
    private int[] costs = { 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
    private string[] lvNames = { "新手", "入门", "钢铁", "青铜", "白银", "黄金", "白金", "钻石", "大师", "宗师" };


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gold = PlayerPrefs.GetInt("gold", gold);
        lv = PlayerPrefs.GetInt("lv", lv);
        exp = PlayerPrefs.GetInt("exp", exp);
        smallTimer = PlayerPrefs.GetInt("scd", smallCount);
        bigTimer = PlayerPrefs.GetInt("bcd", bigCount);
        updateUI();
        AudioManager.Instance.DoMute();
    }
    public void Update()
    {
        changBulletCost();
        fire();
        updateUI();
        changeBg();
    }

    private void updateUI()
    {
        bigTimer -= Time.deltaTime;
        smallTimer -= Time.deltaTime;
        if (smallTimer <= 0)
        {
            smallTimer = smallCount;
            gold += 50;
        }

        if (bigTimer <= 0 && !bigCountButton.gameObject.activeSelf)
        {
            bigCountText.gameObject.SetActive(false);
            bigCountButton.gameObject.SetActive(true);
        }

        while (exp >= 1000 + 200 * lv)
        {
            lv++;
            lvTips.SetActive(true);
            lvTips.transform.Find("LvTipsText").GetComponent<Text>().text = lv.ToString();
            StartCoroutine(lvTips.GetComponent<Effect_HideSelf>().HideSelf(0.6f));
            Instantiate(lvEffect);
            AudioManager.Instance.PlayEffectSound(AudioManager.Instance.lvUpClip);
            exp = exp - (1000 + 200 * lv);

        }
        goldText.text = "$" + gold;
        lvText.text = lv.ToString();
        if ((lv / 10) <= 9)
        {
            lvNameText.text = lvNames[lv / 10];
        }
        else
        {
            lvNameText.text = lvNames[9];
        }

        // Debug.Log(smallTimer + " " + (int)smallTimer / 10 + "  " + (int)smallTimer % 10);
        smallCountText.text = " " + (int)smallTimer / 10 + "  " + (int)smallTimer % 10;
        bigCountText.text = (int)bigTimer + "s";
        expSlider.value = ((float)exp) / (1000 + 200 * lv);
    }

    private void changeBg()
    {
        if (bgIndex != lv /20)
        {
            bgIndex = lv / 20;
            AudioManager.Instance.PlayEffectSound(AudioManager.Instance.seawaveClip);
            Instantiate(seaWaveEffect);
            if (bgIndex >= 3)
            {
                bgImage.sprite = bgSprites[3];
            }
            else
            {
                bgImage.sprite = bgSprites[bgIndex];
            }
            
        }
    }

    private void changBulletCost()
    {
        float move = Input.GetAxis("Mouse ScrollWheel");
        if (move < 0)
        {
            clickSubtract();
        }
        else if (move > 0)
        {
            clickAdd();
        }

    }

    public void clickAdd()
    {
        guns[costIndex / 4].SetActive(false);
        costIndex++;
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.changeClip);
        Instantiate(changEffect);
        costIndex = (costIndex > costs.Length - 1) ? 0 : costIndex;
        guns[costIndex / 4].SetActive(true);
        costText.text = "$" + costs[costIndex];
    }

    public void clickSubtract()
    {
        guns[costIndex / 4].SetActive(false);
        costIndex--;
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.changeClip);
        Instantiate(changEffect);
        costIndex = (costIndex < 0) ? costs.Length - 1 : costIndex;
        guns[costIndex / 4].SetActive(true);
        costText.text = "$" + costs[costIndex];
    }

    private void fire()
    {
        GameObject[] bullets = bullet2;
        int bulletIndex;
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) //按下鼠标左键
        {
            if (gold - costs[costIndex] < 0)
            {
                StartCoroutine(goldNotEnough());
            }
            else
            {
                switch (costIndex / 4)
                {
                    case 0:
                        bullets = bullet2;
                        break;
                    case 1:
                        bullets = bullet3;
                        break;
                    case 2:
                        bullets = bullet4;
                        break;
                    case 3:
                        bullets = bulletS;
                        break;
                    case 4:
                        bullets = bullet1000;
                        break;
                    default:
                        break;
                }
                bulletIndex = lv % 10;
                gold -= costs[costIndex];
                
                GameObject bullet = Instantiate(bullets[bulletIndex]);
                bullet.transform.SetParent(bulletHolder, false);
                bullet.transform.position = guns[costIndex / 4].transform.Find("FirePos").transform.position;
                bullet.transform.rotation = guns[costIndex / 4].transform.Find("FirePos").transform.rotation;
                bullet.AddComponent<Effect_FishAutoMove>().direction = Vector3.up;
                bullet.GetComponent<Effect_FishAutoMove>().speed = 10f;
                bullet.GetComponent<BulletInfo>().damage = costs[costIndex];
                Instantiate(fireEffect, bullet.transform.position, bullet.transform.rotation);
                //Debug.Log("fire Sound!");
                AudioManager.Instance.PlayEffectSound(AudioManager.Instance.fireClip);
            }
            
        }
       
    }

    public void bigCountClickButton()
    {
        gold += 500;
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.rewardClip);
        Instantiate(goldEffect);
        bigCountButton.gameObject.SetActive(false);
        bigCountText.gameObject.SetActive(true);
        bigTimer = bigCount;
    }

    private IEnumerator goldNotEnough()
    {
        goldText.color = goldColor;
        goldText.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        goldText.color = goldColor;
        //Debug.Log(goldColor);
    }
}
