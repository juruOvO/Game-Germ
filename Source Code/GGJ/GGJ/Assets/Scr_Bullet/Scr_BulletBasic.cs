using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scr_BulletBasic : MonoBehaviour
{
    public GameObject prefab;
    public Color color;
    Scr_BulletBase myBase;
    public float Timer = 0;
    private bool hasProcessedCollision = true;
    private List<GameObject> collisions = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        myBase = GetComponent<Scr_BulletBase>();
        color = GetComponent<SpriteRenderer>().color;
    }
    private void Update()
    {
        if(Timer>0)
        Timer -= Time.deltaTime;
    }
    
    void LateUpdate()
    {
        if (!hasProcessedCollision)
        {
            while (collisions.Count > 0 && collisions.First() == gameObject)
            { collisions.Remove(collisions.First());}
            if (collisions.Count > 0)
            {
                if(collisions.First()!=null&&collisions.First().gameObject!=null)
                ProcessCollision(collisions.First());
            }
            collisions.Clear();
            hasProcessedCollision = true;
        }
    }
    void ProcessCollision(GameObject collision)
    {
        //GameObject temp = Instantiate(prefab);
        //temp.transform.position = (transform.position + collision.transform.position) / 2;
        //temp.GetComponent<Scr_BulletBase>().volume = myBase.volume + collision.gameObject.GetComponent<Scr_BulletBase>().volume;
        //temp.GetComponent<Rigidbody2D>().velocity = (GetComponent<Rigidbody2D>().velocity + collision.gameObject.GetComponent<Rigidbody2D>().velocity) / 4;
        //temp.GetComponent<SpriteRenderer>().color = BlendRgbViaCmyk(GetComponent<SpriteRenderer>().color, collision.gameObject.GetComponent<SpriteRenderer>().color);
        //Destroy(collision.gameObject);
        //Destroy(gameObject);
        transform.position = (transform.position + collision.transform.position) / 2;
        myBase.volume = myBase.volume + collision.gameObject.GetComponent<Scr_BulletBase>().volume;
        GetComponent<Rigidbody2D>().velocity = (GetComponent<Rigidbody2D>().velocity + collision.gameObject.GetComponent<Rigidbody2D>().velocity) / 4;
        //GetComponent<SpriteRenderer>().color = BlendRgbViaCmyk(GetComponent<SpriteRenderer>().color, collision.gameObject.GetComponent<SpriteRenderer>().color, 1- collision.gameObject.GetComponent<Scr_BulletBase>().volume /myBase.volume);
        GetComponent<SpriteRenderer>().color = ((myBase.volume- collision.gameObject.GetComponent<Scr_BulletBase>().volume)*GetComponent<SpriteRenderer>().color + collision.gameObject.GetComponent<Scr_BulletBase>().volume*collision.gameObject.GetComponent<SpriteRenderer>().color) / myBase.volume;
        Destroy(collision.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D AnotherCollision)
    {
        if (Timer <= 0)
            if (AnotherCollision.gameObject.tag == "Bullet")
            {
                if (AnotherCollision.transform.position.y > transform.position.y)
                {
                    if(AnotherCollision.gameObject!=gameObject)
                    if (AnotherCollision.gameObject.GetComponent<Scr_BulletBase>().mySpecies == BulletSpecies.Basic)
                    {
                        collisions.Add(AnotherCollision.gameObject);
                            Debug.Log(collisions.Count);
                        hasProcessedCollision = false;

                    }
                }
            }

    }
}
