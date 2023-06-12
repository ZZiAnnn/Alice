using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public static Audio instance; // ����ʵ��
    public AudioSource audiosource; // ��ƵԴ
<<<<<<< HEAD

=======
    public Slider volumeSlider;
>>>>>>> 44604da117c63cdb6947617f73481fd07683924b
    void Awake()
    {
        volumeSlider.value=0.5f;
        // ����Ƿ�������� AudioManager ʵ����������������ٵ�ǰʵ������ֻ֤��һ��ʵ������
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this; // ���õ�ǰʵ��ΪΨһʵ��
        DontDestroyOnLoad(gameObject); // �ڳ����л�ʱ��������Ƶ����������
    }
    private void Update()
    {
        audiosource.volume=volumeSlider.value;
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
