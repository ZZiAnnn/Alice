using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoomHP : MonoBehaviour
{
    //public Image Bar;
    public float hpp = 100;
    // Update is called once per frame
    void Update()
    {
        if (hpp <= 0) Destroy(gameObject);
       // Bar.fillAmount = hpp / 100;
    }

    public void hpdelete(int x)
    {
        hpp = hpp - x;
    }
}
