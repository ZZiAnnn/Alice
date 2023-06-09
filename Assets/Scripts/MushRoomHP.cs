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
    bool flag;
    // Update is called once per frame
    void Start()
    {
        flag = false;
        _material = gameobject.GetComponent<Renderer>().material;
    }
    void Update()
    {
        //OnmouseDown();

        if (hpp <= 0 && !flag) 
        {
            /*_material.DOFloat(30, "_Strength", 0.2f).OnComplete(() =>
            {
                *//*Destroy(gameobject);
                if(particlesystem)
                particlesystem.Play();
                
            });*/
            DOTween.SetTweensCapacity(200, 150);
            var s = DOTween.Sequence();
            
            s.Append(_material.DOFloat(30, "_Strength", 0.2f));
            s.AppendInterval(0.2f);
            s.AppendCallback(() =>
            {
                Destroy(gameobject);
                if (particlesystem) particlesystem.Play();
            });
            flag = true;
            // Bar.fillAmount = hpp / 100;
        }
    }
    public void hpdelete(int x)
    {
        hpp = hpp - x;
    }
    public void OnmouseDown()
    {
        Debug.Log("GetMouseDown");
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("GetMouseDown0");
            _material.DOFloat(30, "_Strength", 0.2f).OnComplete(() =>
            {
                Destroy(gameobject);

                particlesystem.Play();
            });
        }
    }
}
