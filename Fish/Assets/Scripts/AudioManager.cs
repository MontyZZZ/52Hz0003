using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance 
    {
        get
        {
            return instance;
        }
    }

    public AudioSource bgmAudio;
    public AudioClip seawaveClip;
    public AudioClip goldClip;
    public AudioClip rewardClip;
    public AudioClip fireClip;
    public AudioClip changeClip;
    public AudioClip lvUpClip;

    private bool isMute;

    private void Awake()
    {
        instance = this;
        isMute = PlayerPrefs.GetInt("mute") == 0 ? false : true;
        DoMute();
    }

    public void SetMute(bool isOn)
    {
        isMute = !isOn;
        DoMute();
    }
    public void DoMute()
    {
        if (isMute)
        {
            bgmAudio.Pause();
        }
        else
        {
            bgmAudio.Play();
        }
    }

    public bool getMute
    {
        get
        {
            return isMute;
        }
       
    }

    public void PlayEffectSound(AudioClip clip)
    {
        //Debug.Log("PlayEffectSound");
        if (!isMute)
        {
            AudioSource.PlayClipAtPoint(clip, new Vector3(0, 0, -10));
        }
    }
   
}
