using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour
{
    public Toggle muteToggle;
    public GameObject settingPanel;

    private void Start()
    {
        muteToggle.isOn = !AudioManager.Instance.getMute;
    }

    public void SwitchMute(bool isOn)
    {
        AudioManager.Instance.SetMute(isOn);
    }

    public void ClickBackButton()
    {
        PlayerPrefs.SetInt("gold", GameControl.Instance.gold);
        PlayerPrefs.SetInt("lv", GameControl.Instance.lv);
        PlayerPrefs.SetFloat("scd", GameControl.Instance.smallTimer);
        PlayerPrefs.SetFloat("bcd", GameControl.Instance.bigTimer);
        PlayerPrefs.SetInt("exp", GameControl.Instance.exp);
        int temp = (AudioManager.Instance.getMute) ? 1 : 0;
        PlayerPrefs.SetInt("mute", temp);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ClickSettingButton()
    {
        settingPanel.SetActive(true);
    }

    public void ClickCloseButton()
    {
        settingPanel.SetActive(false);
    }
}
