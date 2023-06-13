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
>>>>>>> 4c12f148274950a812c3bfd3d76a8ec8cacba3a3
}
