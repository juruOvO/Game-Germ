using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BulletSwell : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (collision.gameObject.GetComponent<Scr_BulletBase>().mySpecies == BulletSpecies.Basic)
            {
                collision.gameObject.GetComponent<Scr_BulletBase>().volume += 0.2f;
                Destroy(gameObject);
            }

        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

}
