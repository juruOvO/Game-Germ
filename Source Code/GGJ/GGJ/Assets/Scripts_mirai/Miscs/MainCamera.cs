using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : SingletonMonobehaviour<MainCamera>
{
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public void SetTrigger()
    {
        animator.SetTrigger("OnHit");
    }
}
