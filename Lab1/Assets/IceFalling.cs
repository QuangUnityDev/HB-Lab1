using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFalling : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

    }
}
