using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum eColor
{
    Red, Yellow, Blue, Orange, Green, Purple, Black
}
public class Enemy : FSM
{
    [Header("Basic Params")]
    public float HP;
    public float Speed;
    public Vector2 Direction;
    public float Damage;
    public bool isDead;
    public bool isHit;
    public float SpeedCoeff;
    public float harm;
    public eColor color;

    [Header("Attack Setting")]
    public bool isHitTarget;
    public float effectiveDistance;

    [Header("Basic Components")]
    [HideInInspector] public Rigidbody2D rb;

    //[Header("State Machine")]
    //[HideInInspector] public int animState;
    GameObject EnemyOnHitSound;
    GameObject EnemyOnDeadSound;
    GameObject PlayerOnHit;

    public void Awake()
    {
        InitComponent();
        
    }

    

    public override void StatePatrol()
    {
        if (gameObject.transform.position.x > -8)
        {
            if (gameObject.transform.position.y <= effectiveDistance + GLOBAL.LOWERBOUND)
            {
                CurrentState = State.Attack;
            }
            if (isHit) CurrentState = State.Hit;
            Move(GLOBAL.LEFTBOUND, GLOBAL.RIGHTBOUND);
        }
    }
    public override void StateAttack()
    {
        PlayerProperties.HP -= Damage;
        MainCamera.Instance.SetTrigger();
        HP = 0;
        isDead = true;
        CurrentState = State.Dead;
        AudioSource temp = PlayerOnHit.GetComponent<AudioSource>();
        if (temp != null)
        {
            temp.Play();
        }
    }
    public override void StateHit()
    {
        GetHit(harm);
        if (isDead) CurrentState = State.Dead;
        else 
        CurrentState = State.Patrol;
    }

    public override void StateDead()
    {
        Speed = 0;
        Generator.ObjectNum--;
        if (Generator.ObjectNum <= 0)
        {
            Generator.ObjectNum = 0;
        }
        //Dead Effect
        Destroy(gameObject);
        AudioSource tmp = EnemyOnDeadSound.GetComponent<AudioSource>();
        tmp.Play();
    }

    protected virtual void Start()
    {
        CurrentState = State.Patrol;
        Direction.x = 0;
        Direction.y = -0.1f;
        EnemyOnHitSound = GameObject.Find("EnemyOnHitSound");
        EnemyOnDeadSound = GameObject.Find("EnemyOnDeadSound");
        PlayerOnHit = GameObject.Find("PlayerOnHitSound");
    }

    public virtual void GetParams() { }
    public virtual void InitComponent()
    {
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        GetParams();
    }
    public virtual void Move(float LEFT, float RIGHT)
    {
        //if (Mathf.Abs(gameObject.transform.position.x - LEFT) <= 10 || Mathf.Abs(gameObject.transform.position.x - RIGHT) <= 10)
        //{
        //    Direction.x = -Direction.x;
        //}
        Speed = Random.Range(0.5f, 3f) * SpeedCoeff;
        float distance = Speed * Time.deltaTime;
        Vector3 moveVec;
        moveVec.x = distance * Direction.x;
        moveVec.y = distance * Direction.y;
        moveVec.z = 0;
        gameObject.transform.position += moveVec;

    }
    public void refreshColor()
    {
        color = (eColor)Random.Range(0, 3);
        switch (color)
        {
            case eColor.Red:
                GetComponent<SpriteRenderer>().color = new Color(1, 0, 1);
                break;
            case eColor.Yellow:
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 0);
                break;
            case eColor.Blue:
                GetComponent<SpriteRenderer>().color = new Color(0, 1, 1);
                break;
            case eColor.Orange:
                GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f);
                break;
            case eColor.Green:
                GetComponent<SpriteRenderer>().color = new Color(0.5f, 1, 0.5f);
                break;
            case eColor.Purple:
                GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 1);
                break;
            case eColor.Black:
                GetComponent<SpriteRenderer>().color = new Color(0.66667f, 0.66667f, 0.66667f);
                break;
        }
    }
    public void GetHit(float harm)
    {
        HP -= harm;
        //Hit Effect
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        Invoke("wait", 0.2f);
        refreshColor();
        isHit = false;
        if (HP < 0)
        {
            HP = 0;
            isDead = true;
            CurrentState = State.Dead;
        }
        AudioSource tmp = EnemyOnHitSound.GetComponent<AudioSource>();
        tmp.Play();
    }
    public void wait()
    {
        return;
    }

}
