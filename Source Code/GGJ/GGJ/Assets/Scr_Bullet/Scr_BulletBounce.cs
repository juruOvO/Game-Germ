using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BulletBounce : MonoBehaviour
{
    public float k = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(-2 * k * collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);
        }
    }
}
