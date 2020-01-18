﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_FishAutoMove : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 direction = Vector3.right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
