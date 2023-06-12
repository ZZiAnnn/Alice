using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingButton : MonoBehaviour
{
    public GameObject setting;
    private void Start()
    {
        setting.SetActive(false);
    }
    public void pauseButton()
    {
        Time.timeScale = 0;
        setting.SetActive(true);
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
<<<<<<< HEAD
    private void Update()
    {
       //Audio.audiosource.volume=volumeSlider.value;
    }
=======

>>>>>>> 44604da117c63cdb6947617f73481fd07683924b
}
