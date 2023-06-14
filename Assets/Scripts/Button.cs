using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public GameObject first;
    public GameObject setting;
    public GameObject tutoring;

    private AudioClip buttonClickSound;  // 按钮点击声音
    private AudioClip buttonTouchSound;  // 按钮接触声音

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();  // 获取当前物体上的 AudioSource 组件

        buttonClickSound = Resources.Load<AudioClip>("Musics/clickButton"); // 加载音乐文件
        buttonTouchSound = Resources.Load<AudioClip>("Musics/moveToButton");
    }

    public void onclick1()
    {
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void onclick2()
    {
        audioSource.PlayOneShot(buttonClickSound);
        StartCoroutine(DisableSettingObject2(0.2f));
    }

    private IEnumerator DisableSettingObject2(float t)
    {
        yield return new WaitForSeconds(t);
        first.SetActive(false);
        tutoring.SetActive(true);
    }


    public void onclick3()
    {
        audioSource.PlayOneShot(buttonClickSound); // 播放按钮点击声音
        StartCoroutine(DisableSettingObject3(0.2f));

    }

    private IEnumerator DisableSettingObject3(float t)
    {
        yield return new WaitForSeconds(t);  // 延迟等待按钮声音的播放时间
        setting.SetActive(true);
        first.SetActive(false);
    }

    public void onclick4()
    {
        Application.Quit();
        audioSource.PlayOneShot(buttonClickSound);
    }

    public void onclick2_1()
    {
        audioSource.PlayOneShot(buttonClickSound);
        StartCoroutine(DisableSettingObject21(0.2f));
    }

    private IEnumerator DisableSettingObject21(float t)
    {
        yield return new WaitForSeconds(t);
        tutoring.SetActive(false);
        first.SetActive(true);
    }

    public void onclick3_1()
    {
        audioSource.PlayOneShot(buttonClickSound);
        StartCoroutine(DisableSettingObject31(0.2f));
    }

    private IEnumerator DisableSettingObject31(float t)
    {
        yield return new WaitForSeconds(t);
        setting.SetActive(false);
        first.SetActive(true);
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
