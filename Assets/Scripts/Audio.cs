using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio instance; // 单例实例
    public AudioSource audiosource; // 音频源

    void Awake()
    {
        // 检查是否存在其他 AudioManager 实例，如果存在则销毁当前实例，保证只有一个实例存在
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this; // 设置当前实例为唯一实例
        DontDestroyOnLoad(gameObject); // 在场景切换时不销毁音频管理器对象
    }

    // 输入音乐名，播放音乐
    public void PlayMusicByName(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Musics/" + name); // 加载音乐文件
        if (clip != null)
        {
            audiosource.clip = clip;
            audiosource.Play();
        }
        else
        {
            Debug.LogError("音乐文件 " + name + " 不存在或加载失败！");
        }
    }

    // 暂停音乐
    public void PauseMusic()
    {
        if (audiosource.isPlaying)
        {
            audiosource.Pause();
        }
    }

    // 恢复播放音乐
    public void ResumeMusic()
    {
        if (!audiosource.isPlaying)
        {
            audiosource.UnPause();
        }
    }
}
