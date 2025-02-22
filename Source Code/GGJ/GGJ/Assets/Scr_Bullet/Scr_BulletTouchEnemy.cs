using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scr_BulletTouchEnemy : MonoBehaviour
{
    Scr_BulletBase myBase;
    Color myColor;
    void Start()
    {
        myBase = GetComponent<Scr_BulletBase>();
        myColor = GetComponent<SpriteRenderer>().color;
    }
    bool RGBCompare(Color enemyColor)
    {
        myColor = GetComponent<SpriteRenderer>().color;
        if (myColor.r > enemyColor.r ? (myColor.r - enemyColor.r) < 0.1f : (myColor.r - enemyColor.r) > -0.1f)
            if (myColor.g > enemyColor.g ? (myColor.g - enemyColor.g) < 0.1f : (myColor.g - enemyColor.g) > -0.1f)
                if (myColor.b > enemyColor.b ? (myColor.b - enemyColor.b) < 0.1f : (myColor.b - enemyColor.b) > -0.1f)
                {
                    return true;
                }
                else return false;
            else return false;

        else return false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (myBase.mySpecies == BulletSpecies.Basic || myBase.mySpecies == BulletSpecies.Lunge)
            {
                Enemy direEnemy = collision.gameObject.GetComponent<Enemy>();
                if (RGBCompare(collision.gameObject.GetComponent<SpriteRenderer>().color))
                {
                    direEnemy.harm = myBase.volume * 4;
                    direEnemy.isHit = true;
                    Scoreboard.score += (int)(myBase.volume * 10);
                    //direEnemy.GetHit(myBase.volume);
                    Debug.Log(direEnemy.HP);
                    Destroy(gameObject);
                }
                Destroy(gameObject);
            }
        }
    }
}
