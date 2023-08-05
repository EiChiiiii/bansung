using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    float dameRate = 0.5f;
    float nextDamage;

    void Start()
    {
        nextDamage = 0f;
    }

    void Update()
    {
        
    }

    // gay damage
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy" && nextDamage < Time.time)
        {
            EnemyHealth thePlayerHealth = other.gameObject.GetComponent<EnemyHealth> ();
            thePlayerHealth.addDamage (damage);
            nextDamage = dameRate + Time.time;
            Destroy(gameObject);  // Hủy viên đạn khi va chạm với kẻ địch
        }
    }
}
