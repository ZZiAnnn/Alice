using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MushRoomHP : MonoBehaviour
{
    //public Image Bar;
    public float hpp = 100;
    public ParticleSystem particlesystem;
    private Material _material;
    public GameObject gameobject;
    public GameObject coinPrefab;

    bool flag;
    // Update is called once per frame
    void Start()
    {
        flag = false;
        _material = gameobject.GetComponent<Renderer>().material;
    }
    void Update()
    {
        if (hpp <= 0 && !flag)
        {
            DOTween.SetTweensCapacity(200, 150);
            var s = DOTween.Sequence();
            s.Append(_material.DOFloat(30, "_Strength", 0.2f));
            s.AppendInterval(0.2f);
            s.AppendCallback(() =>
            {
                Destroy(gameobject);
                if (particlesystem) particlesystem.Play();

                // 在蘑菇消失的位置生成金币预制体
                if (gameobject.name == "mushroom_2") Instantiate(coinPrefab, new Vector3(transform.position.x, -0.7f, 0), Quaternion.identity);
            });
            flag = true;
        }
    }

    public void hpdelete(int x)
    {
        hpp = hpp - x;
    }
}
