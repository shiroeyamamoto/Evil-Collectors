using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<IInteractObject>() != null)
        {
            collision.transform.GetComponent<IInteractObject>().OnDamaged(GameController.Instance.Player.DamageAttack, false);

            Debug.Log("GameController.Instance.Player.DamageAttack: " + GameController.Instance.Player.DamageAttack);

            Player.Instance.NoneDamage();
        }
    }
}
