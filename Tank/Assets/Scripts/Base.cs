using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public Sprite dieSprite;
    public GameObject boom;
    public AudioClip dieAudio;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void gameOver()
    {
        sr.sprite = dieSprite;
        Instantiate(boom, transform.position, transform.rotation);
        PlayManager.Instance.isDefeat = true;

        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }
}
