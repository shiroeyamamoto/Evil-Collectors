using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashCollison : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") )
        {
            /*if (GameController.Instance.Player.CurrentInfo.mana < GameController.Instance.Player.InfoDefaultSO.mana)
            {
                GameController.Instance.Player.CurrentInfo.mana += 10;
                if (GameController.Instance.Player.CurrentInfo.mana > GameController.Instance.Player.InfoDefaultSO.mana)
                    GameController.Instance.Player.CurrentInfo.mana = GameController.Instance.Player.InfoDefaultSO.mana;
                Player.Instance.OnUpdateMana?.Invoke(Player.Instance.CurrentInfo.mana);
            }*/

            Settings.ememyMiss = false;

            Debug.Log("Cong mana khi dash");

            //PlayerManager.Instance.DashCollison.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
