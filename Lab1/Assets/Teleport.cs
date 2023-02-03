using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform targetTele;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = targetTele.position;
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
    

}
