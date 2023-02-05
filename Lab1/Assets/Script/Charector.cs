using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charector : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] protected HeathBar heathBar;
    [SerializeField] protected CombatText combatTextPrefab;
    protected float hp;
    private string currentAnimName;
    protected bool IsDead => hp <= 0;
    void Start()
    {
        OnInit();
    }
    void Update()
    {
        heathBar.SetNewHp(hp);
    }
    public virtual void OnInit() // Khoi tao mau luc bat dau game
    {
        heathBar.OnInit(100, transform);
        hp = 100;
    }
    public virtual void OnDespawn()
    {
        //OnInit();
    }
    protected virtual void OnDeath()
    {
        ChangeAnim("dead");
    }
    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            //Debug.LogError(animName);
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
    public void OnHit(float damage)
    {       
        Debug.Log("hit");

        if (!IsDead)
        {
            hp -= damage;
            if(hp < damage)
            {
                OnDeath();
                //hp = 0;
            }
            heathBar.SetNewHp(hp);
            Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        }
    }
}
