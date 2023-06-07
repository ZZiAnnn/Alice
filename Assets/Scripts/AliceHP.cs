using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AliceHP : MonoBehaviour
{
    public float HP=100;
    public Slider healthBar;
    void Update()
    {
        healthBar.value=HP/100;
    }
}
