using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField, Range(0f, 20f)] private float pushForce = 2f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!Settings.PlayerDamaged)
            {
                Settings.PlayerDamaged = true;

                EnemyBody enemyBody = collision.gameObject.GetComponent<EnemyBody>();

                Player.Instance.OnDamage();

                Rigidbody2D playerRidid2D = Player.Instance.gameObject.GetComponent<Rigidbody2D>();

                playerRidid2D.velocity = new Vector2(-gameObject.transform.localScale.x*pushForce, 0f);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (Settings.PlayerDamaged)
            {
                Settings.PlayerDamaged = false;

            }
        }
    }
}
