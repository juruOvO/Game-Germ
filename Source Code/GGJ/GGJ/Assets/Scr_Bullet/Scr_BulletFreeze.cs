using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Scr_BulletFreeze : MonoBehaviour
{
    public Rigidbody2D rb;
    public float scale;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
            if (rb.velocity.x > 0)
                rb.velocity -= new Vector2(scale * rb.velocity.x * rb.velocity.x/3 * Time.deltaTime, 0);
            else if (rb.velocity.x < 0)
                rb.velocity += new Vector2(scale * rb.velocity.x * rb.velocity.x/3 * Time.deltaTime, 0);
            if (rb.velocity.y > 0)
                rb.velocity -= new Vector2(0, scale * rb.velocity.y * rb.velocity.y * Time.deltaTime/3);
            else if (rb.velocity.y < 0)
                rb.velocity += new Vector2(0, scale * rb.velocity.y * rb.velocity.y * Time.deltaTime/3);
        }
    }
}
