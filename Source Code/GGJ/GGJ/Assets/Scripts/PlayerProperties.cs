using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    public static float HP;
    public static bool isDead;
    public static float BaseHP;
    public HealthEffect health;
    private void Awake()
    {
        Initiate();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("HP:" + HP);
        health.health = HP / (BaseHP / 100f);
        Debug.Log("HP:" + HP);
        Debug.Log("Health:"+health.health);
        deadState();
        if(isDead)
        {
            SceneControllerManager.Instance.FadeAndLoadScene("GameOver");
        }
    }

    public void Initiate()
    {
        HP = 500.0f;
        BaseHP = HP;
        isDead = false;
    }

    public void deadState()
    {
        if (HP <= 0)
        {
            HP = 0;
            isDead = true;
        }
    }
}
