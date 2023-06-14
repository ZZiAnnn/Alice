using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletShooted : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            Debug.Log("Collision with Barrier detected");
            Destroy(gameObject);
            var mushRoomHP = collision.gameObject.transform.parent.GetComponent<MushRoomHP>();
            if (mushRoomHP != null)
            {
                if(ChangeShader.flag==1) mushRoomHP.hpdelete(100);
                else mushRoomHP.hpdelete(50);
            }
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<FSM>().parameter.health -= 100;
            Destroy(gameObject);
            collision.gameObject.GetComponent<FSM>().parameter.bosshealth.fillAmount =
                collision.gameObject.GetComponent<FSM>().parameter.health * 1.0f /
                collision.gameObject.GetComponent<FSM>().parameter.maxhealth;
            BossDeathControl.hpp = collision.gameObject.GetComponent<FSM>().parameter.health;
        }
    }


}
