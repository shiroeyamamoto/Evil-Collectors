using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Nguyen_Player.Instance.TakeDamage(20);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Stay");
        if (collision.gameObject.CompareTag("Player"))
        {
            Nguyen_Player.Instance.TakeDamage(20);
        }
    }
}
