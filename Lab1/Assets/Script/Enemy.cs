using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Charector
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject attackArea;
    private bool isRight = true;
    private IState currentState;
    private Charector target;
    public Charector Target => target;

    internal void SetTarget(Charector charector)
    {
        this.target = charector;
        if (IsTargetInRange())
        {
            ChangState(new AttackState());
        }
        else if (Target != null)
        {
            ChangState(new PatrolState());
        }
        else
        {
            ChangState(new IdleState());
        }
    }

    private void Update()
    {
        if (currentState != null && !IsDead)
        {
            currentState.OnExcute(this);
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangState(new IdleState());
        DeActiceAttack();
    }
    protected override void OnDeath()
    {
        base.OnDeath();
        ChangState(null);
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(heathBar.gameObject);
        Destroy(gameObject);
    }
    
    public void ChangState(IState newState)
    {
        Debug.Log(newState);
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public void Attack()
    {
        ChangeAnim("attack");
        ActiveAttack();
        Invoke(nameof(DeActiceAttack), 0.5f);
    }
    public void Moving()
    {
        ChangeAnim("run");
        rb.velocity = transform.right * moveSpeed; 
    }
    public void StopMoving()
    {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }
    public bool IsTargetInRange()
    {
        return (target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange);
    }
    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }
    private void DeActiceAttack()
    {
        attackArea.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyWall")
        {
            ChangeDirection(!isRight);
        }
    }
    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }
}
