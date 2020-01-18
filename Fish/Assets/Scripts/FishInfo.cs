using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishInfo : MonoBehaviour
{
    public float hp;
    public int exp;
    public int gold;
    public int MaxNum;
    public int MoveSpeed;

    public GameObject diePrefab;
    public GameObject goldPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(gameObject);
        }
    }

    private void getDamage(int value)
    {
        //Debug.Log(value);
        hp -= value;
        if (hp < 0)
        {
            GameControl.Instance.gold += gold;
            GameControl.Instance.exp += exp;
            
            GameObject die = Instantiate(diePrefab);
            //die.transform.SetParent(gameObject.transform.parent, false);
            die.transform.position = transform.position;
            die.transform.rotation = transform.rotation;

            GameObject goldGo = Instantiate(goldPrefab);
            //die.transform.SetParent(gameObject.transform.parent, false);
            goldGo.transform.position = transform.position;
            goldGo.transform.rotation = transform.rotation;

            if (gameObject.GetComponent<Effect_PlayEffect>())
            {
                AudioManager.Instance.PlayEffectSound(AudioManager.Instance.rewardClip);
                gameObject.GetComponent<Effect_PlayEffect>().PlayEffect();            
            }
            Destroy(gameObject);
        }
    }
}