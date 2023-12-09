using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float pushForce = 2f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            if (Settings.nothingnessSkill || Settings.concentrateSKill)
            {
                Debug.Log("Dang bất tử ");
                return;
            }
            if (!Settings.PlayerDamaged)
            {
                Settings.PlayerDamaged = true;

                Player.Instance.OnDamaged(1);

                float direction = Mathf.Sign(gameObject.transform.localScale.x); // Xác định hướng player

                //Player.Instance.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;

                Player.Instance.rb2d.AddForce(new Vector2(-direction * pushForce, 25f), ForceMode2D.Impulse);

                Debug.Log("Hello Final boss");

                Settings.enterEnemy = true;

                //playerRidid2D.AddForce(new Vector2(-gameObject.transform.localScale.x * pushForce, 25f), ForceMode2D.Impulse);

                //playerRidid2D.velocity = new Vector2(-gameObject.transform.localScale.x*pushForce, 0f);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.activeSelf )
        {
            if (Settings.nothingnessSkill || Settings.concentrateSKill)
            {
                Debug.Log("Dang bất tử ");
                return;
            }
            if (!Settings.PlayerDamaged)
            {
                Settings.PlayerDamaged = true;

                Player.Instance.OnDamaged(1);

                float direction = Mathf.Sign(gameObject.transform.localScale.x); // Xác định hướng player

                //Player.Instance.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;

                Player.Instance.rb2d.AddForce(new Vector2(-direction * pushForce, 25f), ForceMode2D.Impulse);

                Settings.enterEnemy = true;

                //playerRidid2D.AddForce(new Vector2(-gameObject.transform.localScale.x * pushForce, 25f), ForceMode2D.Impulse);

                //playerRidid2D.velocity = new Vector2(-gameObject.transform.localScale.x*pushForce, 0f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
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
