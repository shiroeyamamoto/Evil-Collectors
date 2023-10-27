using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float timeAttack = 0.5f;
    [SerializeField, Range(1f, 10f)] private float timeCooldown = 2f;
    [SerializeField, Range(0f, 10f)] private float normalDamagePercent = 1f;
    [SerializeField, Range(0f, 5f)] private float pushForce = 2f;

    private bool canAttack = true;

    private void Awake()
    {
        canAttack = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnemyBody thisEnemyBody = gameObject.GetComponent<EnemyBody>();
            //Debug.Log("Đang tấn công");
            StartCoroutine(AttackNormal(thisEnemyBody, collision));
            canAttack = false;
        }
    }
    private IEnumerator AttackNormal(EnemyBody thisEnemyBody, Collider2D collision)
    {
        GameObject sword = transform.Find("Attack").gameObject;
        if (canAttack)
        {
            canAttack = false;
            Settings.isAttackNormal = true;

            sword.SetActive(true);
            if (Settings.canParry)
            {
                // khi bị player parry
                // damage parry mặc định bằng 10% sát thương của player + 10% sát thương đòn đánh của enemy
                if (Settings.canParry)
                {
                    thisEnemyBody.EnemyTakeDamge(thisEnemyBody.parryDamaged);
                    Settings.canParry = false;
                }
                Debug.Log("Parry Damage:" + thisEnemyBody.parryDamaged);
            }
            else
            {
                if (!Settings.PlayerDamaged)
                {
                    Settings.PlayerDamaged = true;
                    Debug.Log("Enemy đã đánh trúng player");
                    Player.Instance.TakeDamage(thisEnemyBody.currentDamage * normalDamagePercent);

                    Rigidbody2D playerRidid2D = collision.gameObject.GetComponent<Rigidbody2D>();

                    // Đẩy player khi đánh trúng
                    // Kiểm tra vị trí của player đang ở bên trái hay bên phải enemy
                    // Kiểm tra player đang quay lưng hay đối mặt enemy
                    if (gameObject.transform.position.x < collision.transform.position.x)
                    {
                        if (collision.gameObject.transform.localScale.x > 0)
                        {
                            playerRidid2D.velocity = new Vector2(collision.gameObject.transform.localScale.x * pushForce, 0f);
                        }
                        else
                        {
                            playerRidid2D.velocity = new Vector2(-collision.gameObject.transform.localScale.x * pushForce, 0f);
                        }
                    }
                    else if(gameObject.transform.position.x > collision.transform.position.x)
                    {
                        if (collision.gameObject.transform.localScale.x > 0)
                        {
                            playerRidid2D.velocity = new Vector2(-collision.gameObject.transform.localScale.x * pushForce, 0f);
                        }
                        else
                        {
                            playerRidid2D.velocity = new Vector2(collision.gameObject.transform.localScale.x * pushForce, 0f);
                        }
                    }
                }
                   
            }
            yield return new WaitForSeconds(timeAttack);
            Settings.isAttackNormal = false;
            Settings.PlayerDamaged = false;
            sword.SetActive(false);
            yield return new WaitForSeconds(timeCooldown);
            canAttack = true;
        }
    }
}
