using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
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
        if(other.gameObject.tag == "Player" && nextDamage < Time.time)
        {
            PlayerHealth thePlayerHealth = other.gameObject.GetComponent<PlayerHealth> ();
            thePlayerHealth.addDamage (damage);
            nextDamage = dameRate + Time.time;
            Destroy(gameObject);  // Hủy viên đạn khi va chạm với kẻ địch
        }
    }
}
