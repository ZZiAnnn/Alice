using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public GameObject first;
    public GameObject setting;
    void Start()
    {
        Audio.instance.PlayMusicByName("bgm1");
    }

    public void onclick1()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void onclick2()
    {

    }
    public void onclick3()
    {
        setting.SetActive(true);
        first.SetActive(false);
    }
    public void onclick4()
    {
        Application.Quit();
    }

    public void onclick3_1()
    {
        setting.SetActive(false);
        first.SetActive(true);
    }
}
