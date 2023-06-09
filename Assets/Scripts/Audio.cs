using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio instance; // ����ʵ��
    public AudioSource audiosource; // ��ƵԴ

    void Awake()
    {
        // ����Ƿ�������� AudioManager ʵ����������������ٵ�ǰʵ������ֻ֤��һ��ʵ������
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this; // ���õ�ǰʵ��ΪΨһʵ��
        DontDestroyOnLoad(gameObject); // �ڳ����л�ʱ��������Ƶ����������
    }

    // ��������������������
    public void PlayMusicByName(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Musics/" + name); // ���������ļ�
        if (clip != null)
        {
            audiosource.clip = clip;
            audiosource.Play();
        }
        else
        {
            Debug.LogError("�����ļ� " + name + " �����ڻ����ʧ�ܣ�");
        }
    }

    // ��ͣ����
    public void PauseMusic()
    {
        if (audiosource.isPlaying)
        {
            audiosource.Pause();
        }
    }

    // �ָ���������
    public void ResumeMusic()
    {
        if (!audiosource.isPlaying)
        {
            audiosource.UnPause();
        }
    }
}
