using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_MoveTo : MonoBehaviour
{
    private GameObject goldollect;

    // Start is called before the first frame update
    void Start()
    {
        goldollect = GameObject.Find("GoldCollect");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
                                                 goldollect.transform.position,
                                                 5 * Time.deltaTime);

    }
}
