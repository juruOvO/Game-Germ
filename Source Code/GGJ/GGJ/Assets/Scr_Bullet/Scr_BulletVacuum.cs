using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BulletVacuum : MonoBehaviour
{
    List<GameObject> bullets;
    void Start()
    {
        bullets = new List<GameObject>();
    }
    
    public void Attract()
    {
        foreach (var bullet in bullets) {
            bullet.GetComponent<Rigidbody2D>().velocity = transform.position - bullet.transform.position;
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
