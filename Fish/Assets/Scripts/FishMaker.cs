using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FishMaker : MonoBehaviour
{
    public Transform fishHolder;
    public Transform[] bornPos;
    public GameObject[] fishes;

    public float fishCreateTime = 0.5f;
    public float posCreateTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("makeFish", 0, posCreateTime);
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void makeFish()
    {
        int bornPosIndex = Random.Range(0, bornPos.Length);
        int fishIndex = Random.Range(0, fishes.Length);

        int maxNum = fishes[fishIndex].GetComponent<FishInfo>().MaxNum;
        int maxspeed = fishes[fishIndex].GetComponent<FishInfo>().MoveSpeed;

        int num = Random.Range((maxNum / 2) + 1, maxNum);
        int speed = Random.Range(maxspeed / 2, maxspeed);

        int moveType = Random.Range(0, 2);
        int angOffset;  // 直走的角度
        int angSpeed;   // 转弯速度

        if (moveType == 0)
        {
            //直走
            angOffset = Random.Range(-22, 22);
            StartCoroutine(straigFish(bornPosIndex, fishIndex, num, speed, angOffset));
        }
        else
        {
            //转弯
            if (Random.Range(0, 2) == 0)
            {
                angSpeed = Random.Range(-15, -9);
            }
            else
            {
                angSpeed = Random.Range(9, 15);
            }
            StartCoroutine(turnFish(bornPosIndex, fishIndex, num, speed, angSpeed));
        }
    }

    private IEnumerator straigFish(int posIndex, int fishIndex, int num, int speed, int angOffset)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject fish = Instantiate(fishes[fishIndex]);
            fish.transform.SetParent(fishHolder, false);
            fish.transform.localPosition = bornPos[posIndex].localPosition;
            fish.transform.localRotation = bornPos[posIndex].localRotation;
            fish.transform.Rotate(0, 0, angOffset);
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;
            fish.AddComponent<Effect_FishAutoMove>().speed = speed;
            yield return new WaitForSeconds(fishCreateTime);

        }
    }

    private IEnumerator turnFish(int posIndex, int fishIndex, int num, int speed, int angSpeed)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject fish = Instantiate(fishes[fishIndex]);
            fish.transform.SetParent(fishHolder, false);
            fish.transform.localPosition = bornPos[posIndex].localPosition;
            fish.transform.localRotation = bornPos[posIndex].localRotation;
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;
            fish.AddComponent<Effect_FishAutoMove>().speed = speed;
            fish.AddComponent<Effect_AutoRotate>().speed = angSpeed;
            yield return new WaitForSeconds(fishCreateTime);

        }
    }
}
