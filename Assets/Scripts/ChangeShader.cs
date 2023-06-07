using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShader : MonoBehaviour
{
    //public Camera cam;
    //public Shader shader;
    int flag;        //flag为0时表示可以开启技能，为1时表示正在开启技能/为-1时为技能正在冷却
    float time;
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        flag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && flag == 0) 
        {
            //cam.SetReplacementShader(shader, "Water");
            material.SetColor("_Color", new Color(0.42968f, 0.8671875f, 0.96875f, 0.67578f));
            Debug.Log("!!!");
            flag = 1;
            time = 0;
            
        }
        if (flag != 0)
        {
            time += Time.deltaTime;
        }
        if (time >= 5 && flag == 1) 
        {
            flag = -1;
            material.SetColor("_Color", new Color(0.70703f, 0.88671f, 0.92968f, 0.67578f));
            //cam.ResetReplacementShader();
        }
        else if(time>= 6 && flag == -1)
        {
            flag = 0;
        }
    }
}
