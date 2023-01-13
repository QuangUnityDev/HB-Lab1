using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public GameObject hitVfx;
    public Rigidbody2D rb;
    void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        rb.velocity = transform.right * 5f;
        Invoke(nameof(OnDespawn), 4f);
    }
    private void OnDespawn()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "Enemy")
        {
            Instantiate(hitVfx, transform.position, transform.rotation);
            collision.GetComponent<Charector>().OnHit(30);
            OnDespawn();
        }
    }
}
