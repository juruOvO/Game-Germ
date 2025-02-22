using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletSpecies
{
Basic,Boom,Freeze,Vacuum,Swell,Lunge,Vortex
}
public class Scr_BulletBase : MonoBehaviour
{
    public Rigidbody2D rb;
    public BulletSpecies mySpecies;
    public float up_acceleration=0.1f;
    public float friction = 0.47f;
    public float volume;
    public float stayTimer = 20f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Base_Float();
        Base_Volume();
        Base_Friction(friction);
        stayTimer -= Time.deltaTime;
        if(stayTimer < 0)
            Destroy(gameObject);
    }
    void Base_Volume()
    {
        transform.localScale = Vector3.one * volume/1.73205f;
    }
    void Base_Friction(float scale)
    {
        if(rb.velocity.x>0)
        rb.velocity -= new Vector2(scale * rb.velocity.x * rb.velocity.x*Time.deltaTime, 0);
        else if (rb.velocity.x < 0)
            rb.velocity += new Vector2(scale * rb.velocity.x * rb.velocity.x * Time.deltaTime, 0);
        if (rb.velocity.y > 0)
            rb.velocity -= new Vector2(0, scale * rb.velocity.y * rb.velocity.y * Time.deltaTime/10);
        else if (rb.velocity.y < 0)
            rb.velocity += new Vector2(0, scale * rb.velocity.y * rb.velocity.y * Time.deltaTime/10);
    }
    void Base_Float()
    {
        rb.velocity += new Vector2(0, up_acceleration * Time.deltaTime);
    }
}
