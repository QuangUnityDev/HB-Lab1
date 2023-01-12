using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Charector

{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator anim;
    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttack = false;
    private bool isDead = false;

    private float horizontal;
    private float vertical;
    private string currentAnimName;
    private Vector3 savePoint;
    private int coin;
   
    void Start()
    {
        SavePoint(); 
        OnInit();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }
        isGrounded = CheckGround();
        horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw
        if (isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (isGrounded)
        {
            if (isJumping)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("run");
            }
            if (Input.GetKeyDown(KeyCode.C))
            {             
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                Throw();
            }
        }
        //if (!isGrounded && rb.velocity.y > 0)
        //{
        //    ChangeAnim("jump");
        //    isJumping = true;
        //}
        if (!isGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
            isJumping = false;
        }
        //Moving
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }
        else if (isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.up * rb.velocity.y ;
        }
    }
    private bool CheckGround()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f,Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        return hit.collider != null;
    }
    public override void OnInit()
    {
        base.OnInit();
        isDead = false;
        isAttack = false;
        transform.position = savePoint;
        ChangeAnim("idle");
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
    }
    protected override void OnDeath()
    {
        base.OnDeath();
    }
    private void Attack()
    {
        ChangeAnim("attack");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }
    private void Jump()
    {
        isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);        
    }
    private void Throw()
    {
        ChangeAnim("throw");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }
    private void ResetAttack()
    {
        ChangeAnim("idle");
        isAttack = false;
    }

    internal void SavePoint()
    {
        savePoint = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            coin++;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "DeathZone")
        {
            isDead = true;
            ChangeAnim("dead");
            Invoke(nameof(OnInit), 1f);
        }
    }
}