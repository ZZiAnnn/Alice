using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooted : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("yes");
        if (collision.gameObject.CompareTag("Barrier"))
        {
            Debug.Log("Collision with Barrier detected");
            Destroy(gameObject);
            collision.gameObject.GetComponent<MushRoomHP>().hpdelete(50);
            
        }

    }

}
