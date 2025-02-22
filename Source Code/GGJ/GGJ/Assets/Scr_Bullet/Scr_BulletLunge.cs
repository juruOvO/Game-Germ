using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Scr_BulletLunge : MonoBehaviour
{
    public GameObject prefab;
    public Color color;
    Scr_BulletBase myBase;
    public float Timer = 0;
    private bool hasProcessedCollision = true;
    private List<GameObject> collisions = new List<GameObject>();
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        myBase = GetComponent<Scr_BulletBase>();
        color = GetComponent<SpriteRenderer>().color;
        rb= GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(rb != null) {
        rb.velocity=10*rb.velocity/Vector3.Distance(rb.velocity,Vector3.zero);
        }

        if (Timer > 0)
            Timer -= Time.deltaTime;
    }

    void LateUpdate()
    {
        if (!hasProcessedCollision)
        {
            while (collisions.Count > 0 && collisions.First() == gameObject)
            { collisions.Remove(collisions.First()); }
            if (collisions.Count > 0)
                ProcessCollision(collisions.First());
            collisions.Clear();
            hasProcessedCollision = true;
        }
    }
    void ProcessCollision(GameObject collision)
    {
        if (collision != null)
        {
            transform.position = (transform.position + collision.transform.position) / 2;
            myBase.volume = myBase.volume + collision.gameObject.GetComponent<Scr_BulletBase>().volume;
            GetComponent<Rigidbody2D>().velocity = (GetComponent<Rigidbody2D>().velocity + collision.gameObject.GetComponent<Rigidbody2D>().velocity) / 4;
            GetComponent<SpriteRenderer>().color = ((myBase.volume - collision.gameObject.GetComponent<Scr_BulletBase>().volume) * GetComponent<SpriteRenderer>().color + collision.gameObject.GetComponent<Scr_BulletBase>().volume * collision.gameObject.GetComponent<SpriteRenderer>().color) / myBase.volume;
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D AnotherCollision)
    {
        if (Timer <= 0)
            if (AnotherCollision.gameObject.tag == "Bullet")
            {
                if (AnotherCollision.gameObject != gameObject)
                    if (AnotherCollision.gameObject.GetComponent<Scr_BulletBase>().mySpecies == BulletSpecies.Basic)
                    {
                        collisions.Add(AnotherCollision.gameObject);
                        Debug.Log(collisions.Count);
                        hasProcessedCollision = false;

                    }

            }

    }
}
