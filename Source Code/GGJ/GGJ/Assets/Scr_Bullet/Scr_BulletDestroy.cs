using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BulletDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
