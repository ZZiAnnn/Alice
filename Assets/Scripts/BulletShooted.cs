using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooted : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            collision.gameObject.GetComponent<MushRoomHP>().hpdelete(100);
            Debug.Log("Collision with Barrier detected");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Barrier")
        {
            other.gameObject.GetComponent<MushRoomHP>().hpdelete(100);
        }
        Destroy(gameObject);
        Debug.Log("Collision with Barrier detected");
    }
}
