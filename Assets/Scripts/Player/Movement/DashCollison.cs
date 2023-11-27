using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashCollison : MonoBehaviour
{
    private int currentHealth;

    private void Update()
    {
        if (Settings.isDasing)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            currentHealth = Player.Instance.CurrentInfo.health;
            if (GameController.Instance.Player.CurrentInfo.mana < GameController.Instance.Player.InfoDefaultSO.mana)
            {
                GameController.Instance.Player.CurrentInfo.mana += 10;
                if (GameController.Instance.Player.CurrentInfo.mana > GameController.Instance.Player.InfoDefaultSO.mana)
                    GameController.Instance.Player.CurrentInfo.mana = GameController.Instance.Player.InfoDefaultSO.mana;
                Player.Instance.OnUpdateMana?.Invoke(Player.Instance.CurrentInfo.mana);
            }

            Debug.Log("Cong mana khi dash");

            PlayerManager.Instance.DashCollison.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (currentHealth != Player.Instance.CurrentInfo.health)
            Player.Instance.UseMana(10);
    }
}
