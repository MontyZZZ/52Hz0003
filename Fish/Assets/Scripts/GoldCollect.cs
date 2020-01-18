using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Gold")
        {
            AudioManager.Instance.PlayEffectSound(AudioManager.Instance.goldClip);
            Destroy(collision.gameObject);
        }
    }
}
