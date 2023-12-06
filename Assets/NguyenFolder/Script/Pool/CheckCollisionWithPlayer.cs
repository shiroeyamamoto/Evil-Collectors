using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisionWithPlayer : MonoBehaviour
{
    public float damage;
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Touch something!!!");
        if (collision.tag == "Player")
        {
            Debug.Log("Touch player");
            IInteractObject interactObject = collision.GetComponent<IInteractObject>();
            if (interactObject != null)
            {
                Debug.Log("Touch player 2");
                collision.GetComponent<Player>().OnDamaged(damage);
            }
        }
    }*/
}
