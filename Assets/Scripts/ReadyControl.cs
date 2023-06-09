using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyControl : MonoBehaviour
{
    void Start()
    {
        Audio.instance.PlayMusicByName("bgm2");
    }

    public void delete()
    {
        Destroy(this.gameObject);
    }
}
