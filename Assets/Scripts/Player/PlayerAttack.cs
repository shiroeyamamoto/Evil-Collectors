using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    /// <summary>
    /// Đòn tấn công trúng enemy gây sát thương
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyBody enemyBody = collision.gameObject.GetComponent<EnemyBody>();

            enemyBody.EnemyTakeDamge(Nguyen_Player.Instance.DamageAttack);

            Nguyen_Player.Instance.NoneDamage();
        }   
    }
}
