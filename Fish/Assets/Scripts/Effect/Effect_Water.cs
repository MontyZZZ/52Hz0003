using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Water : MonoBehaviour
{

    public Texture[] textures;
    private Material material;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        InvokeRepeating("changeTexture", 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void changeTexture()
    {
        material.mainTexture = textures[index];
        index = (index + 1) % textures.Length;
    }
}
