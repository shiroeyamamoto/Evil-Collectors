using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField, Range(0f, 5f)] private float pushForce = 0.5f;

    [HideInInspector] public bool inForwardAttack = false;
    [HideInInspector] public bool inRetreatAttack = false;

    // Va chạm của đòn tấn công tới đối thủ

    /// <summary>
    /// Đòn tấn công trúng enemy gây sát thương
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Settings.canKnockback = true;

            EnemyBody enemyBody = collision.gameObject.GetComponent<EnemyBody>();

            enemyBody.EnemyTakeDamge(GameController.Instance.Player.DamageAttack);

            Rigidbody2D enemyRigid2D = collision.gameObject.GetComponent<Rigidbody2D>();

            // Đẩy kẻ địch khi đánh trúng
            if (Player.Instance.transform.position.x < collision.transform.position.x)
            {
                enemyRigid2D.velocity = new Vector2(collision.gameObject.transform.localScale.x * pushForce, 0f);
            }
            else if(Player.Instance.transform.position.x > collision.transform.position.x)
            {
                enemyRigid2D.velocity = new Vector2(-collision.gameObject.transform.localScale.x * pushForce, 0f);
            }

            Player.Instance.NoneDamage();
        }   
    }
}
