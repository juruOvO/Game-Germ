using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BulletVortex : MonoBehaviour
{
    List<GameObject> bullets;
    void Start()
    {
        bullets = new List<GameObject>();
    }

    public void Attract()
    {
        foreach (var bullet in bullets)
        {
            if (bullet.GetComponent<Rigidbody2D>() != null)
            {
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bullet.GetComponent<Rigidbody2D>().velocity.x, -1.5f);
                bullet.GetComponent<Scr_BulletBase>().stayTimer += 20f;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            bullets.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            bullets.Remove(collision.gameObject);
        }
    }
}
