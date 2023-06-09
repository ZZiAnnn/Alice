using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                mushRoomHP.hpdelete(50);
            }
        }
    }


}
