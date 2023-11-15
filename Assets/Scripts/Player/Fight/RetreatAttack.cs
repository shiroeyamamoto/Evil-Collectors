using UnityEngine;

public class RetreatAttack : MonoBehaviour
{
    [SerializeField] PlayerAttack playerAttack;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // chắc chắn đang trong phạm vi tấn công
            playerAttack.inForwardAttack = false;
            if (!playerAttack.inRetreatAttack)
            {
                playerAttack.inRetreatAttack = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAttack.inForwardAttack = false;
            playerAttack.inRetreatAttack = false;
        }
    }
}
