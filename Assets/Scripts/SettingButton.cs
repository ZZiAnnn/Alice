using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingButton : MonoBehaviour
{
    private AudioClip buttonClickSound;  // ��ť�������
    public GameObject setting;
    private AudioSource audioSource;

    private void Start()
    {
        setting.SetActive(false);
        audioSource = GetComponent<AudioSource>();  // ��ȡ��ǰ�����ϵ� AudioSource ���
        buttonClickSound = Resources.Load<AudioClip>("Musics/clickButton"); // ���������ļ�
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
