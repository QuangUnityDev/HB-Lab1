using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    [SerializeField] private Image imageFill;
    [SerializeField] private Vector3 offset;
    private Transform target;
    float hp;
    float maxHp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp / maxHp, Time.deltaTime * 5);
        transform.position = target.position + offset;
    }
    public void OnInit(float maxHp,Transform target)
    {
        this.target = target;
        this.maxHp = maxHp;
        hp = maxHp;
        imageFill.fillAmount = 1;
    }
    public void SetNewHp(float hp)
    {
        this.hp = hp;
        //imageFill.fillAmount = hp / maxHp;  
    }
}
