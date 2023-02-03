using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Charector

{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;
    [SerializeField] private float speedRope;
    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttack = false;
    //private bool isDead = false;
    private bool isRope = false;
    private float horizontal;
    //private float vertical;
    //private string currentAnimName;
    private Vector3 savePoint;
    private int coin = 0;
    

    private void Awake()
    {
        coin = PlayerPrefs.GetInt("coin", 0);
    }
    void Update()
    {
        //Debug.Log(CheckGround());
        if (IsDead)
        {
            return;
        }
        isGrounded = CheckGround();
        //horizontal = Input.GetAxisRaw("Horizontal");
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
            //if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            //{
            //    Jump();
            //}
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
        //if (isRope)
        //{
        //    UpRope();
        //}
        if (!isGrounded && rb.velocity.y > 0 && isRope == false)
        {
            ChangeAnim("jump");
            isJumping = true;
        }
        if (!isGrounded && rb.velocity.y < 0 && isRope == false)
        {
            ChangeAnim("fall");
            isJumping = false;
        }
        //Moving
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }
        else if (isGrounded && isAttack == false && !isJumping)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.up * rb.velocity.y ;
        }
        
        UIManager.instance.SetCoin(coin);
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
        //isDead = false;
        isAttack = false;
        transform.position = savePoint;
        DeActiceAttack();
        ChangeAnim("idle");
        SavePoint();
        UIManager.instance.SetCoin(coin);
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
    }
    protected override void OnDeath()
    {
        base.OnDeath();
        OnInit();
    }
    private bool isConditionAttack()
    {
        return isJumping == true || isGrounded == false || isAttack == true;
    }
    public void Attack()
    {
        if (isConditionAttack())
        {
            return;
        }
        ChangeAnim("attack");
        isAttack = true;
        ActiveAttack();
        Invoke(nameof(ResetAttack), 0.5f);        
        Invoke(nameof(DeActiceAttack), 0.5f);
    }
    public void UpRope()
    {
        
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * speedRope * Time.deltaTime);
        //rb.velocity = new Vector2(rb.velocity.x, vertical * speedRope * Time.deltaTime);
        //Debug.Log(isRope);
    }
    public void Jump()
    {
        if (CheckGround() && isRope == false && isJumping == false)
        {
            isJumping = true;
            ChangeAnim("jump");
            rb.AddForce(jumpForce * Vector2.up);
        }
        if (isRope)
        {
            rb.gravityScale = 0;
            UpRope();
        }       
    }
    
    public void Throw()
    {        
        if(isConditionAttack())
        {
            return;
        }
        ChangeAnim("throw");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
        Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
    }
    private void ResetAttack()
    {
        ChangeAnim("idle");
        isAttack = false;
    }
    public void SetMove(float horizontal)
    {
        this.horizontal = horizontal;
    }
    internal void SavePoint()
    {
        savePoint = transform.position;
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
        if (collision.tag == "Coin")
        {
            coin++;
            PlayerPrefs.SetInt("coin", coin);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "DeathZone")
        {
            //isDead = true;
            ChangeAnim("dead");
            Invoke(nameof(OnInit), 1f);
        }
        if (collision.gameObject.CompareTag("Rope"))
        {
            transform.SetParent(collision.gameObject.transform);
            isRope = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Rope"))
        {
            transform.gameObject.transform.SetParent(null);
            isRope = false;
            rb.gravityScale = 1;
        }
        //Debug.Log(isRope);
    }
    [SerializeField] private GameObject Shield;
    public bool isShield = false;
    public void ShieldOnPlayer()
    {
        isShield = true;
        Shield.gameObject.SetActive(true);
        Invoke(nameof(ShieldOffPlayer), 5f);
    }
    private void ShieldOffPlayer()
    {
        Shield.gameObject.SetActive(false);
        isShield = false;
    }
}