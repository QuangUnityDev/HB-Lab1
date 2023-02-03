using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField]Player isShield;
    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        //Debug.Log(Player.is);
        if ((collision.tag == "Player" && isShield.isShield == false) || collision.tag == "Enemy")
        {
            collision.GetComponent<Charector>().OnHit(30f);
            //Debug.Log("SwordHit");
        }
    }
}
