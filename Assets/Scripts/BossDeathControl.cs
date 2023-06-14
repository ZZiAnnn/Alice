using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathControl : MonoBehaviour
{
    public static float hpp = 1000;
    public ParticleSystem particlesystem;
    public GameObject gameobject;
    bool flag;
    public GameObject gameobject2;
    Transform tran_1;
    // Update is called once per frame
    void Start()
    {
        flag = false;
    }
    void Update()
    {
        if (hpp <= 0 && !flag) 
        {
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
