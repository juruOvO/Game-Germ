using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BulletBoom : MonoBehaviour
{
    public GameObject prefab;
    public GameObject prefabSwell;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (collision.gameObject.GetComponent<Scr_BulletBase>().mySpecies == BulletSpecies.Basic)
            {
                for(float i= collision.gameObject.GetComponent<Scr_BulletBase>().volume; i > 0; i -= 0.05f)
                {
                    GameObject temp = Instantiate(prefab);
                    Vector2 direction = new Vector2(Random.Range(-0.99f, 0.99f), Random.Range(-0.99f, 0.99f));
                    temp.transform.position = collision.transform.position +(Vector3)direction*0.1f;
                    temp.GetComponent<Rigidbody2D>().velocity = direction;
                    temp.GetComponent<Scr_BulletBasic>().Timer = 0.2f;
                    temp.GetComponent<Scr_BulletBase>().volume = 0.1f;
                    Destroy(temp.GetComponent<Scr_SetColor>());
                    temp.GetComponent<SpriteRenderer>().color = collision.gameObject.GetComponent<SpriteRenderer>().color;
                }  
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else if(collision.gameObject.GetComponent<Scr_BulletBase>().mySpecies == BulletSpecies.Lunge)
            {
                for (float i = collision.gameObject.GetComponent<Scr_BulletBase>().volume; i > 0; i -= 0.05f)
                {
                    GameObject temp = Instantiate(prefab);
                    Vector2 direction = new Vector2(Random.Range(-0.99f, 0.99f), Random.Range(-0.99f, 0.99f));
                    temp.transform.position = collision.transform.position + (Vector3)direction * 0.1f;
                    temp.GetComponent<Rigidbody2D>().velocity = direction;
                    temp.GetComponent<Scr_BulletBasic>().Timer = 0.2f;
                    Scr_BulletBase tempBase = temp.GetComponent<Scr_BulletBase>();
                    tempBase.volume = 0.1f;
                    tempBase.friction = 0;
                    tempBase.up_acceleration = 0;
                    temp.GetComponent<SpriteRenderer>().color = collision.gameObject.GetComponent<SpriteRenderer>().color;
                }
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else if(collision.gameObject.GetComponent<Scr_BulletBase>().mySpecies == BulletSpecies.Swell)
            {
                for (float i = collision.gameObject.GetComponent<Scr_BulletBase>().volume; i > 0; i -= 0.05f)
                {
                    GameObject temp = Instantiate(prefabSwell);
                    Vector2 direction = new Vector2(Random.Range(-0.99f, 0.99f), Random.Range(-0.99f, 0.99f));
                    temp.transform.position = collision.transform.position + (Vector3)direction * 0.1f;
                    temp.GetComponent<Rigidbody2D>().velocity = direction;
                    temp.GetComponent<Scr_BulletBase>().volume = 0.1f;
                }
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }

        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        
    }
}
