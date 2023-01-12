using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Charector
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float attackRange;
    [SerializeField]private float moveSpeed;
    private bool isRight = true;
    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExcute(this);
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangState(new IdleState());
    }
    protected override void OnDeath()
    {
        base.OnDeath();
        ChangState(new IdleState());
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
    }
    private IState currentState;
    public void ChangState(IState newState)
    {
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
    public bool TargetInRange()
    {
        return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyWall")
        {
            ChangeDirection(!isRight);
        }
    }
    private void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }
}
