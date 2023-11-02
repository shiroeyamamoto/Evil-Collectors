using Unity.VisualScripting;
using UnityEngine;

public class ForwardAttack : MonoBehaviour
{
    [SerializeField] PlayerAttack playerAttack;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // chắc chắn forward attack chỉ hoạt động khi ngoài phạm vi tấn công 
            if (!playerAttack.inRetreatAttack)
            {
                if (!playerAttack.inForwardAttack)
                {
                    playerAttack.inForwardAttack = true;
                }
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAttack.inForwardAttack = false;
        }
    }
}
