using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
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

    }
    public void onclick4()
    {
        Application.Quit();
    }
}
