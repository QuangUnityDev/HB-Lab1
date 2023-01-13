using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charector : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private float hp;
    private string currentAnimName;
    private bool IsDead => hp <= 0;
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void OnInit()
    {
        hp = 100;
    }
    public virtual void OnDespawn()
    {

    }
    protected virtual void OnDeath()
    {
        ChangeAnim("dead");
        Invoke(nameof(OnDespawn), 2f);
    }
    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
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
            }
        }
    }
}
