using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathControl : MonoBehaviour
{
    public AudioClip deathSound;  //À¿Õˆ…˘“Ù
    private AudioSource audioSource;
    public static float hpp = 1000;
    public ParticleSystem particlesystem;
    public GameObject gameobject;
    bool flag;
    public GameObject gameobject2;
    Transform tran_1;
    // Update is called once per frame
    void Start()
    {
        Audio.instance.PlayMusicByName("bgm3");
        flag = false;
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (hpp <= 0 && !flag) 
        {
            audioSource.PlayOneShot(deathSound);
            particlesystem.transform.position = gameobject.transform.position;
            Destroy(gameobject, 0.1f);
            DOTween.SetTweensCapacity(200, 150);
            var s = DOTween.Sequence();
            s.AppendInterval(0.1f);
            s.AppendCallback(() =>
            {
                if (particlesystem) particlesystem.Play();            
            });
            flag = true;
            gameobject2.SetActive(true);
        }
    }
}
