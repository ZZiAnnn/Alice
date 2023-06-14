using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingButton : MonoBehaviour
{
    private AudioClip buttonClickSound;  // 按钮点击声音
    public GameObject setting;
    private AudioSource audioSource;

    private void Start()
    {
        setting.SetActive(false);
        audioSource = GetComponent<AudioSource>();  // 获取当前物体上的 AudioSource 组件
        buttonClickSound = Resources.Load<AudioClip>("Musics/clickButton"); // 加载音乐文件
    }
    public void pauseButton()
    {
        setting.SetActive(true);
        Time.timeScale = 0; 
    }
    public void backButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("startPage");
    }
    public void continueButton()
    {
        Time.timeScale = 1;
        setting.SetActive(false);
    }

    public void onClickEnd()
    {
        audioSource.PlayOneShot(buttonClickSound);
        Application.Quit();
    }

    public void onClickReplay()
    {
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene("gameScene_1");
    }

    public void onClickReturn()
    {
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene("startPage");
    }
}
