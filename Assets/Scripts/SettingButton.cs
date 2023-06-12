using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingButton : MonoBehaviour
{
    public GameObject setting;
    public Slider volumeSlider;
    private void Start()
    {
        volumeSlider.value=0.5f;
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
    private void Update()
    {
       //Audio.audiosource.volume=volumeSlider.value;
    }
}
