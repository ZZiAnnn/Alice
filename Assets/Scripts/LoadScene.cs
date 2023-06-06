using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Slider slider;
    public Transform trans;
    public Transform trans_2;
    float initx;
    bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0;
        initx = trans.position.x;
        StartCoroutine(LoadLevel());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Alice"))
        {
            slider.value = (trans.position.x - initx) / (trans_2.position.x - initx - 0.255f);
        }
        else
        {
            slider.value = 1;
        }
    }
    IEnumerator LoadLevel()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            if ((slider.value >= 1.0f || !GameObject.Find("Alice")) && operation.progress >= 0.9f) 
            {
                flag = true;
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
