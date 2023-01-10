using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed;
    [SerializeField] private Animator anim;
    private bool isGround;
    private bool isJumping;
    private bool isAttack;
   
    private float horizontal;
    private float vertical;
    private string currentAnimName;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(CheckGround());
        isGround = CheckGround();
        horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw
        if (Mathf.Abs(horizontal) > 0.1f )
        {
            ChangeAnim("Run");
            rb.velocity = new Vector2(horizontal * Time.deltaTime * speed, rb.velocity.y);
        }
        else if(isGround)
        {
            ChangeAnim("Idle");
            rb.velocity = Vector2.zero;
        }
    }
    private bool CheckGround()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f,Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        return hit.collider != null;
    }
    void Attack()
    {

    }
    void Jump()
    {

    }
    void Throw()
    {

    }
    private void ChangeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}