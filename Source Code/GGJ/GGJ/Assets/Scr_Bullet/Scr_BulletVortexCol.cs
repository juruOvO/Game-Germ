using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BulletVortexCol : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            transform.GetChild(0).GetComponent<Scr_BulletVortex>().Attract();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            transform.GetChild(0).GetComponent<Scr_BulletVortex>().Attract();
            Destroy(gameObject);
        }
    }
}
